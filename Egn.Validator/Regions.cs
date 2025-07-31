using Egn.Validator.Models;

namespace Egn.Validator
{
    sealed class Regions
    {
        public static readonly Region[] List = new Region[]
        {
            new Region { Key = "EGN_VAL.REGIONS.BLAGOEVGRAD", Name = "Благоевград", Code = 43 },            /* от 000 до 043 */ 
            new Region { Key = "EGN_VAL.REGIONS.BURGAS", Name = "Бургас", Code = 93 },                      /* от 044 до 093 */
            new Region { Key = "EGN_VAL.REGIONS.VARNA", Name = "Варна", Code = 139 },                       /* от 094 до 139 */ 
            new Region { Key = "EGN_VAL.REGIONS.VELIKO_TARNOVO", Name = "Велико Търново", Code = 169 },     /* от 140 до 169 */
            new Region { Key = "EGN_VAL.REGIONS.VIDIN", Name = "Видин", Code = 183 },                       /* от 170 до 183 */
            new Region { Key = "EGN_VAL.REGIONS.VRATSA", Name = "Враца", Code = 217 },                      /* от 184 до 217 */
            new Region { Key = "EGN_VAL.REGIONS.GABROVO", Name = "Габрово", Code = 233 },                   /* от 218 до 233 */
            new Region { Key = "EGN_VAL.REGIONS.KARDJALI", Name = "Кърджали", Code = 281 },                 /* от 234 до 281 */
            new Region { Key = "EGN_VAL.REGIONS.KUSTENDIL", Name = "Кюстендил", Code = 301 },               /* от 282 до 301 */
            new Region { Key = "EGN_VAL.REGIONS.LOVECH", Name = "Ловеч", Code = 319 },                      /* от 302 до 319 */
            new Region { Key = "EGN_VAL.REGIONS.MONTANA", Name = "Монтана", Code = 341 },                   /* от 320 до 341 */
            new Region { Key = "EGN_VAL.REGIONS.PAZARDJIK", Name = "Пазарджик", Code = 377 },               /* от 342 до 377 */
            new Region { Key = "EGN_VAL.REGIONS.PERNIK", Name = "Перник", Code = 395 },                     /* от 378 до 395 */ 
            new Region { Key = "EGN_VAL.REGIONS.PLEVEN", Name = "Плевен", Code = 435 },                     /* от 396 до 435 */ 
            new Region { Key = "EGN_VAL.REGIONS.PLOVDIV", Name = "Пловдив", Code = 501 },                   /* от 436 до 501 */ 
            new Region { Key = "EGN_VAL.REGIONS.RAZGRAD", Name = "Разград", Code = 527 },                   /* от 502 до 527 */ 
            new Region { Key = "EGN_VAL.REGIONS.RUSE", Name = "Русе", Code = 555 },                          /* от 528 до 555 */ 
            new Region { Key = "EGN_VAL.REGIONS.SILISTRA", Name = "Силистра", Code = 575 },                 /* от 556 до 575 */ 
            new Region { Key = "EGN_VAL.REGIONS.SLIVEN", Name = "Сливен", Code = 601 },                     /* от 576 до 601 */ 
            new Region { Key = "EGN_VAL.REGIONS.SMOLYAN", Name = "Смолян", Code = 623 },                    /* от 602 до 623 */ 
            new Region { Key = "EGN_VAL.REGIONS.SOFIA.GRAD", Name = "София - град", Code = 721 },           /* от 624 до 721 */ 
            new Region { Key = "EGN_VAL.REGIONS.SOFIA.OKRAG", Name = "София - окръг", Code = 751 },         /* от 722 до 751 */
            new Region { Key = "EGN_VAL.REGIONS.STARA_ZAGORA", Name = "Стара Загора", Code = 789 },         /* от 752 до 789 */  
            new Region { Key = "EGN_VAL.REGIONS.DOBRICH", Name = "Добрич", Code = 821 },                    /* от 790 до 821 */ 
            new Region { Key = "EGN_VAL.REGIONS.TARGOVISHTE", Name = "Търговище", Code = 843 },             /* от 822 до 843 */ 
            new Region { Key = "EGN_VAL.REGIONS.HASKOVO", Name = "Хасково", Code = 871 },                   /* от 844 до 871 */ 
            new Region { Key = "EGN_VAL.REGIONS.SHUMEN", Name = "Шумен", Code = 903 },                      /* от 872 до 903 */ 
            new Region { Key = "EGN_VAL.REGIONS.YAMBOL", Name = "Ямбол", Code = 925 },                      /* от 904 до 925 */ 
            new Region { Key = "EGN_VAL.REGIONS.UNKNOWN", Name = "Друг/Неизвестен", Code = 999 },           /* от 926 до 999 - Такъв регион понякога се ползва при
                                                                                                            // родени преди 1900, за родени в чужбина
                                                                                                            // или ако в даден регион се родят повече
                                                                                                            // деца от предвиденото. Доколкото ми е
                                                                                                            // известно няма правило при ползването
                                                                                                            // на 926 - 999 */
        };
    }
}