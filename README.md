# XmlValidator

**Overview**
- XML Validator is a C# project that identifies whether or not an XML string is valid XML, without using RegEx or System.XML. It is designed to provide developers with a simple and easy way to validate XML strings without the use of System.XML or Regular Expressions.

**Features**

- Validates syntax and structure of XML string.
- Unit Test package

**Requirements** 
- .NET 7.0 Visual Studio 2022

**Usage**
- To use the XML Validator, simply call DetermineXml() with the XML string as the parameter. The method will return a boolean determining whether or not the XML string is valid.

**Testing**
- The Solution comes with a Unit Test Project that can be utilized to test the functionality of the program.


```csharp
string xmlString = "<Design><Code>Hello World</Code></Design>";

bool isValid = XmlValidator.ValidateXml(xmlString);
```
