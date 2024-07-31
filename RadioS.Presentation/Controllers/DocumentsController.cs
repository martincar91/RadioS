using MediatR;
using Microsoft.AspNetCore.Mvc;
using RadioS.Application.Entities;
using RadioS.Application.Models;
using RadioS.Presentation.Models;
using System.Net;
using System.Reflection;
using System.Text.RegularExpressions;

namespace RadiosS.ProjectRadioS.WebAPI.Controllers;

[Route("/api/[controller]")]
public class DocumentsController : Controller
{
    private ISender Sender;
    public DocumentsController(ISender sender)
    {
        Sender = sender;
    }
    [HttpGet]
    public async Task<IActionResult> GetFinancialDocument([FromQuery] GetDocumentRequestModel request)
    {
        #region product validation
        var isProductSupported = new IsProductSupported.Query(request.ProductCode);
        var productValiation = await Sender.Send(isProductSupported);

        if (!productValiation.isSupported)
            return StatusCode((int)HttpStatusCode.Forbidden, "Product is not supported");
        #endregion product validation

        #region tenant validation
        var isWhitelistedTenant = new IsWhitelistedTenant.Query(request.TenantId);
        var tenantValidation = await Sender.Send(isWhitelistedTenant);

        if (!tenantValidation.isWhitelisted)
            return StatusCode((int)HttpStatusCode.Forbidden, "Tenant is not whitelisted");
        #endregion tenant validation

        #region client fetch and validation
        var getClientIdAndVAT = new GetClientIdAndVAT.Query(request.TenantId, request.DocumentId);
        var clientIdAndVATInfo = await Sender.Send(getClientIdAndVAT);

        /*Not sure why tenantId is required here.
        Maybe if we have a lot of tenants so we can select tenant first
        and then search the clients within that tenant. I will not use tenantId here since we have
        only two of them but I am still sending tenantId in the query */
        var isWhitelistedClient = new IsWhitelistedClient.Query(request.TenantId, clientIdAndVATInfo.client.CliendId);
        var result = await Sender.Send(isWhitelistedClient);
        if (!result.isWhitelisted)
            return StatusCode((int)HttpStatusCode.Forbidden, "Client is not whitelisted");
        #endregion client fetch and validation

        #region fetch additional client information
        var getAdditionalClientInfo = new GetAdditionalClientInfo.Query(clientIdAndVATInfo.client.ClientVAT);
        var additionalClientInfo = await Sender.Send(getAdditionalClientInfo);

        if (additionalClientInfo.client.CompanyType == "small")
            return StatusCode((int)HttpStatusCode.Forbidden, "Company type is small");

        #endregion fetch additional client information

        #region fetch financial document and anonymization
        var documentByIdQuery = new GetDocumentById.Query(request.TenantId, request.DocumentId);
        var document = Sender.Send(documentByIdQuery);

        var anonimizeQuery = new AnonymizeData.Query(document.Result.financialDocument, request.ProductCode);
        var anonymizedDocument = Sender.Send(anonimizeQuery);

        var response = new EnrichedDocumentModel
        {
            Data = anonymizedDocument.Result.financialDocument,
            Company = new CompanyInfoModel
            {
                CompanyType = additionalClientInfo.client.CompanyType,
                RegistrationNumber = additionalClientInfo.client.RegistrationNumber
            }
        };
        #endregion fetch financial document


        return Ok(response);
    }
}
