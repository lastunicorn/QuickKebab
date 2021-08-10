// Quick Kebab
// Copyright (C) 2021 Dust in the Wind
// 
// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
// 
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
// 
// You should have received a copy of the GNU General Public License
// along with this program.  If not, see <http://www.gnu.org/licenses/>.

using System;
using System.Collections.Generic;
using System.Linq;

namespace DustInTheWind.QuickKebab.Domain
{
    public class KebabCase
    {
        private string decodedText;
        private string encodedText;

        public HashSet<char> CharsToEncode { get; } = new HashSet<char>
        {
            ' ',
            '/',
            '[',
            ']',
            '(',
            ')',
            '{',
            '}'
        };

        public event EventHandler Decoded;
        public event EventHandler Encoded;

        public string DecodedText
        {
            get => decodedText;
            set
            {
                if (decodedText == value)
                    return;

                decodedText = value;

                encodedText = ToKebabCase(decodedText);
                OnEncoded();
            }
        }

        public string EncodedText
        {
            get => encodedText;
            set
            {
                if (encodedText == value)
                    return;

                encodedText = value;

                IEnumerable<char> decodedChars = FromKebabCase(encodedText);
                decodedText = new string(decodedChars.ToArray());
                OnDecoded();
            }
        }

        private string ToKebabCase(string input)
        {
            string result = input.ToLower();

            foreach (char charToEncode in CharsToEncode)
                result = result.Replace(charToEncode, '-');

            return result;
        }

        private static IEnumerable<char> FromKebabCase(string input)
        {
            bool allowDecodeChar = true;

            foreach (char c in input)
            {
                if (c == '-')
                {
                    if (allowDecodeChar)
                    {
                        yield return ' ';
                        allowDecodeChar = false;
                    }
                    else
                    {
                        yield return c;
                        allowDecodeChar = true;
                    }
                }
                else
                {
                    yield return c;
                    allowDecodeChar = true;
                }
            }
        }

        protected virtual void OnDecoded()
        {
            Decoded?.Invoke(this, EventArgs.Empty);
        }

        protected virtual void OnEncoded()
        {
            Encoded?.Invoke(this, EventArgs.Empty);
        }
    }
}