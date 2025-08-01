# Egn.Validator
## Simple project for Bulgarian personal Identification number validation on C Sharp

This project is basically translated **PHP** project from [Georgi Chorbadzhiyski](https://georgi.unixsol.org " Georgi Chorbadzhiyski"), for the original project [click here](https://georgi.unixsol.org/programs/egn.php "click here")
You can find the information, about how the validation is processed in [Wikipedia](https://bg.wikipedia.org/wiki/Единен_граждански_номер "Wikipedia") as well


------------
Find in: [NuGet](https://www.nuget.org/packages/Bulgarian.Egn.Validator) as **Bulgarian.Egn.Validator**


## Usege:

### Validation
- As method -> 
```csharp
string egn = "0021010029";
bool validEgn = Egn.isValid(egn);
// validEgn = true

string invalidEgn = "0022290048"; // The year is not leap, so February has only 28 days
validEgn = Egn.isValid(invalidEgn);
// validEgn = false
```


- As ValidationAttribute ->

*default*
```csharp
public class Bulgarian
{
    public string FirstName { get; set; }

    public string LastName { get; set; }

    [EGN]
    public string PersonalId { get; set; } // Validation error of -> "PersonalId is invalid."
}
```

*with custom error message*
```csharp
public class Bulgarian
{
    public string FirstName { get; set; }

    public string LastName { get; set; }

    [EGN(ErrorMessage="Your error message")]
    public string PersonalId { get; set; } // Validation error of -> "Your error message"
}
```

*with custom error message and Display Name interpolation*
```csharp
public class Bulgarian
{
    public string FirstName { get; set; }

    public string LastName { get; set; }
    [DisplayName("Personal ID")]
    [EGN(ErrorMessage="The field for \'{0}\' is invalid!")]
    public string PersonalId { get; set; } // Validation error of -> "The field for 'Personal ID' is invalid!"
}
```



### Extra functionality
- **Egn.TryGetInfo**
```csharp
string egn = "0021010029";
EgnInfo info;
if (Egn.TryGetInfo(egn, out info)) 
{
    // info.Gender = 1 (male)
    // info.Region = Region {
    //      Key = "EGN_VAL.REGIONS.BLAGOEVGRAD",
    //      Name = "Благоевград",
    //      StartCode = 0,
    //      Code = 43
    // },
    // info.Age = (TimeSpan from now, so future EGN will be with negative sign),
    // info.Birthday = (DateOnly -> 1st of January, 1800)
}

```

- **Egn.Generate**	
```csharp
var egn1 = Egn.Generate(); // returns "9110201743" (randomized result)

uint day = 1;
uint mon = 1;
uint year = 1800;
byte gender = 1;
string region = "EGN_VAL.REGIONS.BLAGOEVGRAD"
var egn2 = Egn.Generate(day, mon, year, gender, region); // returns "0021010029" (not granted result, it could be any of the available region values for this gender)

```

------------


### Region Class

Represents a geographical region with localization support.

#### Properties

| Property | Type | Description |
|----------|------|-------------|
| `Key` | `string` | Translation key used for internationalization/localization purposes |
| `Name` | `string` | Region name in Bulgarian language |
| `StartCode` | `int` | Numeric code that represents the first intended identifier for the region |
| `Code` | `int` | Numeric code that represents the last intended identifier for the region |

#### Usage Example

```csharp
var region = new Region
{
    Key = "EGN_VAL.REGIONS.SOFIA.GRAD",
    Name = "София - град",
    StartCode = 624,
    Code = 721
};

var region2 = new Region
{
    Key = "EGN_VAL.REGIONS.MONTANA",
    Name = "Монтана",
    StartCode = 320,
    Code = 341
};
```

---

### EgnInfo Class

Contains information extracted from a Bulgarian EGN (Единен граждански номер - Unified Civil Number), which is the Bulgarian national identification number system.

#### Properties

| Property | Type | Description |
|----------|------|-------------|
| `Gender` | `byte` | Gender indicator: `0` for Male, `1` for Female |
| `Region` | `Region` | The region where the person was registered, represented by a `Region` object |
| `BornBefore` | `int` | Indicates how much people of the same gender are born before that one on that day |
| `Age` | `TimeSpan` | Current age of the person represented as a time span |
| `Birthday` | `DateOnly` | Date of birth extracted from the EGN |

#### Usage Example

```csharp
var egnInfo = new EgnInfo
{
    Gender = 0, // Male
    Region = new Region { Key = "EGN_VAL.REGIONS.STARA_ZAGORA", Name = "Стара Загора", StartCode = 752, Code = 789 },
    BornBefore = 5,
    Age = TimeSpan.FromDays(365 * 25), // Approximately 25 years
    Birthday = new DateOnly(1998, 5, 15)
};
```

#### Notes

- The `Gender` property uses numeric representation where `0` = Male and `1` = Female
- The `Region` property links to the geographical region associated with the EGN
- The class is designed to parse and represent data from Bulgarian national identification numbers