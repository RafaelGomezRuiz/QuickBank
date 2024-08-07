﻿namespace QuickBank.Core.Application.Helpers
{
    public static class CodeStringGenerator
    {
        private static Random random = new Random();
        private const string Uppercase = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        private const string Lowercase = "abcdefghijklmnopqrstuvwxyz";
        private const string Numbers = "0123456789";
        private const string SpecialCharacters = "!@#$%^&*()_+[]{}|;:,.<>?";

        public static string GeneratePassword()
        {
            string password =
                Convert.ToChar(GetRandomCharacter(Uppercase)).ToString() +
                Convert.ToChar(GetRandomCharacter(Lowercase)).ToString() +
                Convert.ToChar(GetRandomCharacter(Numbers)).ToString() +
                Convert.ToChar(GetRandomCharacter(SpecialCharacters)).ToString();

            string allCharacters = Uppercase + Lowercase + Numbers + SpecialCharacters;
            for (int i = password.Length; i < 12; i++)
            {
                password += GetRandomCharacter(allCharacters);
            }

            return new string(password.ToCharArray().OrderBy(c => random.Next()).ToArray());
        }

        public static string GenerateProductNumber()
        {
            string numberGenerated = string.Empty;

            for (int i = 0; i < BusinessLogicConstantsHelper.MaxLengthNumberAccount; i++)
            {
                numberGenerated += GetRandomCharacter(Numbers);
            }

            return numberGenerated;
        }

        private static char GetRandomCharacter(string characters)
        {
            int index = random.Next(characters.Length);
            return characters[index];
        }
    }
}
