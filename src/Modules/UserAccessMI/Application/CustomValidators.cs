using System.Linq.Expressions;
using CompanyName.MyMeetings.Modules.UserAccessMI.Domain.ErrorHandling;
using CSharpFunctionalExtensions;
using FluentValidation;

namespace CompanyName.MyMeetings.Modules.UserAccessMI.Application
{
    public static class CustomValidators
    {
        public static IRuleBuilderOptions<T, TProperty> CustomNotNull<T, TProperty>(this IRuleBuilder<T, TProperty> ruleBuilder)
        {
            return DefaultValidatorExtensions.NotNull(ruleBuilder)
                .WithMessage(Errors.General.ValueIsRequired().Serialize());
        }

        public static IRuleBuilderOptions<T, TProperty> CustomNotEmpty<T, TProperty>(this IRuleBuilder<T, TProperty> ruleBuilder)
        {
            return DefaultValidatorExtensions.NotEmpty(ruleBuilder)
                .WithMessage(Errors.General.ValueIsRequired().Serialize());
        }

        public static IRuleBuilderOptions<T, string> CustomLength<T>(this IRuleBuilder<T, string> ruleBuilder, int min, int max)
        {
            return DefaultValidatorExtensions.Length(ruleBuilder, min, max)
                .WithMessage(Errors.General.InvalidLength().Serialize());
        }

        public static IRuleBuilderOptions<T, string> CustomMaximumLength<T>(this IRuleBuilder<T, string> ruleBuilder, int maximumLength)
        {
            return DefaultValidatorExtensions.MaximumLength(ruleBuilder, maximumLength)
                .WithMessage(Errors.General.InvalidLength(maxLength: maximumLength).Serialize());
        }

        public static IRuleBuilderOptions<T, TProperty> CustomGreaterThanOrEqualTo<T, TProperty>(this IRuleBuilder<T, TProperty> ruleBuilder, TProperty valueToCompare)
            where TProperty : IComparable<TProperty>, IComparable
        {
            return DefaultValidatorExtensions.GreaterThanOrEqualTo(ruleBuilder, valueToCompare)
                .WithMessage(Errors.General.ValueIsInvalid().Serialize());
        }

        public static IRuleBuilderOptions<T, int?> CustomGreaterThanOrEqualTo<T>(this IRuleBuilder<T, int?> ruleBuilder, int valueToCompare)
        {
            return DefaultValidatorExtensions.GreaterThanOrEqualTo(ruleBuilder, valueToCompare)
                .WithMessage(Errors.General.ValueIsInvalid().Serialize());
        }

        public static IRuleBuilderOptions<T, TProperty> CustomGreaterThan<T, TProperty>(this IRuleBuilder<T, TProperty> ruleBuilder, TProperty valueToCompare)
            where TProperty : IComparable<TProperty>, IComparable
        {
            return DefaultValidatorExtensions.GreaterThan(ruleBuilder, valueToCompare)
                .WithMessage(Errors.General.ValueIsInvalid().Serialize());
        }

        public static IRuleBuilderOptions<T, string> CustomEmailAddress<T>(this IRuleBuilder<T, string> ruleBuilder)
        {
            return DefaultValidatorExtensions.EmailAddress(ruleBuilder)
                .WithMessage(Errors.General.ValueIsInvalid().Serialize());
        }

        public static IRuleBuilderOptions<T, string> CustomEqual<T>(this IRuleBuilder<T, string> ruleBuilder, Expression<Func<T, string>> expression, IEqualityComparer<string>? comparer = null)
        {
            return DefaultValidatorExtensions.Equal(ruleBuilder, expression, comparer)
                .WithMessage(Errors.General.ValueIsInvalid().Serialize());
        }

        public static IRuleBuilderOptions<T, TElement> MustBeValueObject<T, TElement, TValueObject>(
            this IRuleBuilder<T, TElement> ruleBuilder,
            Func<TElement, Result<TValueObject, Error>> factoryMethod)
            where TValueObject : ValueObject
        {
            return (IRuleBuilderOptions<T, TElement>)ruleBuilder.Custom((value, context) =>
            {
                Result<TValueObject, Error> result = factoryMethod(value);

                if (result.IsFailure)
                {
                    if (result.Error.Errors != null)
                    {
                        foreach (var error in result.Error.Errors)
                        {
                            context.AddFailure(error.Serialize());
                        }
                    }

                    if (result.Error.Code != null)
                    {
                        context.AddFailure(result.Error.Serialize());
                    }
                }
            });
        }

        public static IRuleBuilderOptionsConditions<T, IList<TElement>> ListMustContainNumberOfItems<T, TElement>(
            this IRuleBuilder<T, IList<TElement>> ruleBuilder, int? min = null, int? max = null)
        {
            return ruleBuilder.Custom((list, context) =>
            {
                if (min.HasValue && list.Count < min.Value)
                {
                    context.AddFailure(Errors.General.CollectionIsTooSmall(min.Value, list.Count).Serialize());
                }

                if (max.HasValue && list.Count > max.Value)
                {
                    context.AddFailure(Errors.General.CollectionIsTooLarge(max.Value, list.Count).Serialize());
                }
            });
        }
    }
}