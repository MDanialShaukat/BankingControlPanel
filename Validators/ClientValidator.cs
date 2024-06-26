using FluentValidation;
using BankingControlPanel.Models;

namespace BankingControlPanel.Validators
{
    public class ClientValidator : AbstractValidator<Client>
    {
        public ClientValidator()
        {
            /*
             *Email: Should be required and email format.
             *First name: Should be required and less than 60 characters.
             *Last name: Should be required and less than 60 characters.
             *Mobile number: Should be correct format with country code (you can use some library as well).
             *Personal id: Should be required and it should be exactly 11 characters.
             *Sex: Should be required.
             *Accounts: At least one account is required.
             */
            RuleFor(client => client.Email).NotEmpty().EmailAddress();
            RuleFor(client => client.FirstName).NotEmpty().MaximumLength(60);
            RuleFor(client => client.LastName).NotEmpty().MaximumLength(60);
            RuleFor(client => client.PersonalId).NotEmpty().Length(11);
            RuleFor(client => client.MobileNumber).NotEmpty().Matches(@"^\+\d{1,3}\d{9,15}$");
            RuleFor(client => client.Sex).NotEmpty();
            RuleFor(client => client.Accounts).NotEmpty().WithMessage("At least one account is required.");
        }
    }
}
