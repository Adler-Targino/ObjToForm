# ObjToForm &middot; [![License: MIT](https://img.shields.io/badge/License-MIT-green.svg)](./LICENSE) ![.NET](https://img.shields.io/badge/.NET-512BD4?logo=dotnet&logoColor=white) ![C#](https://img.shields.io/badge/C%23-239120?logo=c-sharp&logoColor=white)

ObjToForm is a .NET tool for building HTML forms from objects meant to accelerate testing without spending time writing HTML Code.

## Quick start

Several quick start options are available:

- Install with [NuGet](https://www.nuget.org/): `Install-Package ObjToForm -Version 1.0.0`
- Install with .NET CLI: `dotnet add package ObjToForm --version 1.0.0`
- Clone the repo: `git clone https://github.com/Adler-Targino/ObjToForm.git`

## Demo

### Example object
```csharp

    public class DemoObj()
    {
        public string PublicString;
        public int PublicInt;
        public DateTime PublicDateTime;
        public float PublicFloat;
        public bool PublicBool;
        private string PrivateString;
        public DemoOptions PublicOptionsEnum;
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
    @ObjToForm.ConvertToRawHtmlForm(Model.Obj)
</form>

<hr>

<!-- Bootstrap-styled form -->
<form method="post">
    @ObjToForm.ConvertToBootstrapForm(Model.Obj)
</form>

```

### Result

Plain HTML Form

<img width="349" height="379" alt="image" src="https://github.com/user-attachments/assets/b8d84c81-ed14-4a4f-b8bf-0dfaf7f6b376" />


Bootstrap-styled form

<img width="1367" height="480" alt="image" src="https://github.com/user-attachments/assets/7ea47cdc-ba28-4946-a261-8421e158fcb6" />

## Notes
- As shown in the previous example, only public attributes are rendered.
- Using Bootstrap-styled forms requires to previously add bootstrap to your project

