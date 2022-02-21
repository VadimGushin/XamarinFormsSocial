using System;
using System.Collections.Generic;

namespace XamarinSocial.Helpers
{
    public static class UserHeightHelper
    {
        #region Constants

        private const int _minHeight = 3;
        private const int _maxHeight = 8;
        private const int _count = 11;
        private const double _footCoef = 30.48;
        private const double _inchCoef = 2.54;

        #endregion

        #region Methods
        public static List<string> GetUserHeightSource()
        {
            var result = new List<string>();
            for (int i = _minHeight; i < _maxHeight; i++)
            {
                for (int j = 0; j <= _count; j++)
                {
                    string inches = $"{j}\"";
                    if (j == 0)
                    {
                        inches = string.Empty;
                    }
                    result.Add($"{i}'{inches}({GetHeightValue(i,j)}cm)");
                }
            }
            result.Add($"{_maxHeight}'({244}cm)");
            return result;
        }

        private static int GetHeightValue(int foots, int inches)
        {
            return (int)Math.Round(foots * _footCoef + inches * _inchCoef);
        }

        #endregion
    }
}
