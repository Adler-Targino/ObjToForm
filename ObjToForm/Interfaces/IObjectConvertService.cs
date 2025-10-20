using Microsoft.AspNetCore.Html;

namespace ObjToForm.Interfaces
{
    internal interface IObjectConvertService
    {
        IHtmlContent ConvertToForm(object obj, string prefix ,bool ModelBinding);
    }
}
