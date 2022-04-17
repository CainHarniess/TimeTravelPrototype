using System;
using System.Text;

namespace Osiris.Utilities
{
    public static class StringExtensions
    {
        public static string ToEditorName(this string name)
        {

            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("Input string is empty or whitespace.");
            }
            
            StringBuilder output = new StringBuilder(name.Length);

            for (int i = 0; i < name.Length; i++)
            {
                if (i == 0)
                {
                    char firstCharacter = name[i];

                    if (firstCharacter == '_')
                    {
                        continue;
                    }

                    if (char.IsLetter(firstCharacter) && char.IsLower(firstCharacter))
                    {
                        output.Append(char.ToUpper(firstCharacter));
                        continue;
                    }
                }

                char currentCharacter = name[i];

                if (IsUppercaseLetter(currentCharacter) || char.IsDigit(currentCharacter))
                {
                    output.Append($" {currentCharacter}");
                    continue;
                }

                output.Append(currentCharacter);
            }

            return output.ToString();
        }

        private static bool IsUppercaseLetter(char letter)
        {
            return char.IsLetter(letter) && char.IsUpper(letter);
        }
    }
}
