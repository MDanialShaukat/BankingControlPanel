﻿using FluentValidation;
using BankingControlPanel.Models;

namespace BankingControlPanel.Validators
{
    public class ClientValidator : AbstractValidator<Client>
    {
        public ClientValidator()
        {
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
