using System;

namespace MovingCursor.Utilities
{
    internal class Validator
    {
        public static int ValidateInt(object data, Func<int, bool> predicate, string errorMessage, string parametrName)
        {
            if (data == null)
                throw new ArgumentNullException(parametrName, "Не заполнена обязательная переменная.");

            if (data is int value)
            {
                if (!predicate(value))
                    throw new FormatException($"Параметр {parametrName}: {errorMessage}");
                return value;
            }
            else
            {
                throw new InvalidCastException($"Параметр {parametrName}: Значение должно быть целочисленным.");
            }
        }
    }
}
