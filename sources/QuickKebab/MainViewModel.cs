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

namespace QuickKebab
{
    internal class MainViewModel : ViewModelBase
    {
        private readonly KebabCase kebabCase;

        private string decodedText;
        private string encodedText;

        public string DecodedText
        {
            get => decodedText;
            set
            {
                decodedText = value;
                OnPropertyChanged();

                kebabCase.DecodedText = decodedText;
            }
        }

        public string EncodedText
        {
            get => encodedText;
            set
            {
                encodedText = value;
                OnPropertyChanged();

                kebabCase.EncodedText = encodedText;
            }
        }

        public MainViewModel()
        {
            kebabCase = new KebabCase();

            kebabCase.Decoded += HandleKebabCaseDecoded;
            kebabCase.Encoded += HandleKebabCaseEncoded;
        }

        private void HandleKebabCaseDecoded(object? sender, EventArgs e)
        {
            DecodedText = kebabCase.DecodedText;
        }

        private void HandleKebabCaseEncoded(object? sender, EventArgs e)
        {
            EncodedText = kebabCase.EncodedText;
        }
    }
}