# ObjToForm &middot; [![License: MIT](https://img.shields.io/badge/License-MIT-green.svg)](./LICENSE) ![.NET](https://img.shields.io/badge/.NET-512BD4?logo=dotnet&logoColor=white) ![C#](https://img.shields.io/badge/C%23-239120?logo=c-sharp&logoColor=white)

ObjToForm is a .NET tool for building HTML forms from objects meant to accelerate testing without spending time writing HTML Code.

## Quick start

Several quick start options are available:

- Install with [NuGet](https://www.nuget.org/): `Install-Package ObjToForm`
- Install with .NET CLI: `dotnet add package ObjToForm`
- Clone the repo: `git clone https://github.com/Adler-Targino/ObjToForm.git`

## Demo

### Example object
```csharp

    public class DemoObj
    {
        public string PublicString { get; set; }
        public int PublicInt { get; set; }
        public DateTime PublicDateTime { get; set; }
        public float PublicFloat { get; set; }
        public bool PublicBool { get; set; }
        private string IShouldntBeHere {get; set;}
        public DemoOptions PublicOptionsEnum { get; set; }
    }

    public enum DemoOptions
    {
        Option1,
        Option2,
        Option3,
        Option4,
        Option5
    }

```


After installing the library you can convert C# objects directly into HTML forms

### Razor Page Example

```cshtml

@using ObjToForm;

...

<!-- Raw HTML form -->
<form method="post">
    @ObjToForm.ConvertToRawHtmlForm(typeof(DemoObj))
    <!-- Or if you are using Model Binding -->
    @ObjToForm.ConvertToRawHtmlForm(typeof(DemoObj), "Obj", true)
</form>

<!-- Or using Model Binding -->

<hr>

<!-- Bootstrap-styled form -->
<form method="post">
    @ObjToForm.ConvertToBootstrapForm(typeof(DemoObj))
    <!-- Or if you are using Model Binding -->
    @ObjToForm.ConvertToBootstrapForm(typeof(DemoObj), "Obj", true)
</form>

```

### Result

Plain HTML Form

<img width="349" height="379" alt="image" src="https://github.com/user-attachments/assets/b8d84c81-ed14-4a4f-b8bf-0dfaf7f6b376" />


Bootstrap-styled form

<img width="1367" height="480" alt="image" src="https://github.com/user-attachments/assets/7ea47cdc-ba28-4946-a261-8421e158fcb6" />

### Receive the values in your controller as usual

```csharp
public IActionResult OnPost(string PublicString, int PublicInt, DateTime PublicDateTime, float PublicFloat, bool PublicBool, DemoOptions PublicOptionsEnum)
{
    ...
    return Page();
}

//Or, if you are using ModelBinding
public IActionResult OnPost(DemoObj Obj)
{
    ...
    return Page();
}

```
## Notes
- As shown in the previous example, only public properties are rendered.
- Using Bootstrap-styled forms requires to previously add bootstrap to your project

---
# Advanced Usage

ObjToForm has a few more tools that can be helpfull if you intend to create more elaborated forms

## Customization with Attributes

Attributes can be applied directly to the class properties, making it possible to define CSS classes, inline styles, labels, or even custom HTML attributes for the rendered elements.

### Available Attributes

- **[DivClass("className", bool override = false)]**  
  Sets the CSS class(es) for the `<div>` wrapping the field.  
  The `override` parameter defines whether to replace the default classes (in case of using `ConvertToBootstrapForm`) or just append them.

- **[LabelClass("className", bool override = false)]**  
  Sets the CSS class(es) for the `<label>` of the field.

- **[InputClass("className", bool override = false)]**  
  Sets the CSS class(es) for the `<input>` or `<select>` element.

- **[DivStyle("style")]**  
  Adds inline CSS styles to the `<div>`.

- **[LabelStyle("style")]**  
  Adds inline CSS styles to the `<label>`.

- **[InputStyle("style")]**  
  Adds inline CSS styles to the `<input>`.

- **[Label(bool enabled)]**  
  Hides the label when `enabled = false`.

- **[Label("text")]**  
  Sets the text of the label.

- **[HtmlAttribute("attribute=value")]**  
  Adds custom HTML attributes directly to the field (e.g., `maxlength=50`, `data-custom="123"`).

Quick Note: Multiple attributes (including attributes of the same type) can be assigned for each property

### Example Object 

```csharp
public class CustomDemoObj
{
    [Label("Full Name")]
    [InputClass("form-control mb-2")]
    [DivClass("col-md-6")]
    [HtmlAttribute("placeholder=Enter your name")]
    public string Name { get; set; }

    [Label("Birth Date")]
    [InputClass("form-control")]
    [InputStyle("max-width:200px;")]
    public DateTime BirthDate { get; set; }

    [Label("Email", true)]
    [InputClass("form-control")]
    [HtmlAttribute("type=email")]
    public string Email { get; set; }

    [Label("Notes")]
    [InputClass("form-control")]
    [HtmlAttribute("rows=4")]
    public string Notes { get; set; }
}
```
Calling the `ConvertToRawHtmlForm` method while passing this object as a paramter will result in the following result:

<img width="1349" height="447" alt="image" src="https://github.com/user-attachments/assets/16ec6b2a-1c8c-456a-aeaa-accf2e4cd2e0" />

