using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Web;

namespace code_4_lazy
{
    public static class SeoUrlHelper
    {
        public static string FriendlyUrl(this string text)
        {
            var decomposed = text.Normalize(NormalizationForm.FormKD);
            var builder = new StringBuilder();
            foreach (var ch in decomposed)
            {
                var charInfo = CharUnicodeInfo.GetUnicodeCategory(ch);
                switch (charInfo)
                {
                    // Keep these as they are
                    case UnicodeCategory.DecimalDigitNumber:
                    case UnicodeCategory.LetterNumber:
                    case UnicodeCategory.LowercaseLetter:
                    case UnicodeCategory.CurrencySymbol:
                    case UnicodeCategory.OtherLetter:
                    case UnicodeCategory.OtherNumber:
                        builder.Append(ch);
                        break;

                    // Convert these to dashes
                    case UnicodeCategory.DashPunctuation:
                    case UnicodeCategory.MathSymbol:
                    case UnicodeCategory.ModifierSymbol:
                    case UnicodeCategory.OtherPunctuation:
                    case UnicodeCategory.OtherSymbol:
                    case UnicodeCategory.SpaceSeparator:
                        builder.Append('-');
                        break;

                    // Convert to lower-case
                    case UnicodeCategory.TitlecaseLetter:
                    case UnicodeCategory.UppercaseLetter:
                        builder.Append(char.ToLowerInvariant(ch));
                        break;

                    // Ignore certain types of characters
                    case UnicodeCategory.OpenPunctuation:
                    case UnicodeCategory.ClosePunctuation:
                    case UnicodeCategory.ConnectorPunctuation:
                    case UnicodeCategory.Control:
                    case UnicodeCategory.EnclosingMark:
                    case UnicodeCategory.FinalQuotePunctuation:
                    case UnicodeCategory.Format:
                    case UnicodeCategory.InitialQuotePunctuation:
                    case UnicodeCategory.LineSeparator:
                    case UnicodeCategory.ModifierLetter:
                    case UnicodeCategory.NonSpacingMark:
                    case UnicodeCategory.OtherNotAssigned:
                    case UnicodeCategory.ParagraphSeparator:
                    case UnicodeCategory.PrivateUse:
                    case UnicodeCategory.SpacingCombiningMark:
                    case UnicodeCategory.Surrogate:
                        break;
                }
            }

            var built = builder.ToString();
            while (built.Contains("--"))
                built = built.Replace("--", "-");
            while (built.EndsWith("-"))
            {
                built = built.Substring(0, built.Length - 1);
            }
            while (built.StartsWith("-"))
            {
                built = built.Substring(1, built.Length - 1);
            }
            return built;
        }
    }
}