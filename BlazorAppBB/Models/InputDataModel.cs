using System.ComponentModel.DataAnnotations;

namespace BlazorAppBB.Models
{
    public class InputDataModel
    {
        [Required(ErrorMessage = "Please provide your name"), MinLength(1),MaxLength(1000, ErrorMessage = "Please limit your input to 1000 characters")]
        public string sName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Please provide the date")]
        public DateOnly sMonth { get; set; } = DateOnly.FromDayNumber(0);

        [Required, Range(0.1, 9999999.99,ErrorMessage = "Please enter a number between 0.1 and 9999999.99")]
        public double dData1 { get; set; } = 0.0;
        [Required, Range(0.1, 9999999.99, ErrorMessage = "Please enter a number between 0.1 and 9999999.99")]

        public double dData2 { get; set; } = 0.0;
        [Required, Range(0.1, 9999999.99, ErrorMessage = "Please enter a number between 0.1 and 9999999.99")]

        public double dData3 { get; set; } = 0.0;
    }
}
