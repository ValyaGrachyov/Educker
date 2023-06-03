using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Diagnostics.CodeAnalysis;
using System.Text.RegularExpressions;

namespace Eduker.ViewModels.PurchaseVm;

public class PurchaseVm
{
    public string Email { get; set; }
    public string Date { get; set; }
    public string CVC { get; set; }
    public string CardNumber { get; set; }

    public string IsNormal()
    {
        bool isEmail;
        bool isDate;
        bool isCvc;
        bool isCardNumber;
        try
        {
            isEmail = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$").IsMatch(Email);
            isDate = new Regex(@"^0[1-9]|[10]|[11]|[12]/[0-9]{2}$").IsMatch(Date);
            isCvc = new Regex(@"^[0-9]{3}$").IsMatch(CVC);
            isCardNumber = new Regex(@"^[0-9]{16}$").IsMatch(CardNumber);
        }
        catch (Exception e)
        {
            return "Values can't be null";
        }
        if (!isEmail) return "Wrong email format";
        if (!isDate) return "Wrong date format. **/**";
        if (!isCvc) return "Wrong cvc format. Need 3 numbers";
        if (!isCardNumber) return "Wrong card number format. Need 16 numbers";
        return null;
    }
}