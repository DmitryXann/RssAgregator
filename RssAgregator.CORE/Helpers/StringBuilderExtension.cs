using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RssAgregator.CORE.Helpers
{
    internal static class StringBuilderExtension
    {
        public static int FirstIndexOf(this StringBuilder stringBuilder, string valueToSearch, bool ignorCase = false)
        {
            var result = -1;
            var bufferIndex = 0;

            for (var index = 0; index < stringBuilder.Length; index++)
            {
                if (ignorCase ? char.ToLower(stringBuilder[index]) == char.ToLower(valueToSearch[bufferIndex]) : stringBuilder[index] == valueToSearch[bufferIndex])
                {
                    if (bufferIndex == valueToSearch.Length - 1)
                    {
                        result = index - bufferIndex;
                        break;
                    }
                    else
                    {
                        bufferIndex++;
                    }
                }
                else
                {
                    bufferIndex = 0;

                    if (ignorCase ? char.ToLower(stringBuilder[index]) == char.ToLower(valueToSearch[bufferIndex]) : stringBuilder[index] == valueToSearch[bufferIndex])
                    {
                        bufferIndex++;
                    }
                }
            }

            return result;
        }

        public static int FirstIndexOfAny(this StringBuilder stringBuilder, bool ignorCase, params string[] valueToSearch)
        {
            var firstFoundedIndex = -1;

            var searchIndex = 0;
            while (searchIndex < valueToSearch.Length && firstFoundedIndex < 0)
            {
                firstFoundedIndex = FirstIndexOf(stringBuilder, valueToSearch[searchIndex++], ignorCase);
            }

            return firstFoundedIndex;
        }

        public static int LastIndexOf(this StringBuilder stringBuilder, string valueToSearch, bool ignorCase = false)
        {
            var result = -1;
            var bufferIndex = valueToSearch.Length;

            for (var index = stringBuilder.Length - 1; index > 0; index--)
            {
                if (ignorCase ? char.ToLower(stringBuilder[index]) == char.ToLower(valueToSearch[bufferIndex - 1]) : stringBuilder[index] == valueToSearch[bufferIndex - 1])
                {
                    if (bufferIndex - 1 == 0)
                    {
                        result = index;
                        break;
                    }
                    else
                    {
                        bufferIndex--;
                    }
                }
                else
                {
                    bufferIndex = valueToSearch.Length;

                    if (ignorCase ? char.ToLower(stringBuilder[index]) == char.ToLower(valueToSearch[bufferIndex - 1]) : stringBuilder[index] == valueToSearch[bufferIndex - 1])
                    {
                        bufferIndex--;
                    }
                }
            }

            return result;
        }

        public static int LastIndexOfAny(this StringBuilder stringBuilder, bool ignorCase, params string[] valueToSearch)
        {
            var lastFoundedIndex = -1;

            var searchIndex = 0;
            while (searchIndex < valueToSearch.Length && lastFoundedIndex < 0)
            {
                lastFoundedIndex = LastIndexOf(stringBuilder, valueToSearch[searchIndex++], ignorCase);
            }

            return lastFoundedIndex;
        }

        public static IEnumerable<StringBuilder> Split(this StringBuilder stringBuilder, Func<char, bool> splitCriterea, Func<char, bool> saveSplittingCharacter, bool trimData = true)
        {
            var result = new List<StringBuilder>();

            var lastSplitIndex = 0;
            for (var index = 0; index < stringBuilder.Length; index++)
            {
                if (splitCriterea(stringBuilder[index]))
                {
                    var length = index - lastSplitIndex;
                    var saveSplitCharacter = saveSplittingCharacter(stringBuilder[index]);

                    if (length > (saveSplitCharacter ? 1 : 0)) 
                    {
                        var range = stringBuilder.GetRange(lastSplitIndex, length + (saveSplitCharacter ? 1 : 0));
                        var trimmedRange = trimData ? range.Trim() : range;

                        if (trimmedRange.Length > 0)
                        {
                            result.Add(trimmedRange);
                        }

                        lastSplitIndex = index + (saveSplitCharacter ? 1 : 0);
                    }
                }
            }

            return result;
        }

        public static StringBuilder GetRange(this StringBuilder stringBuilder, int startIndex, int length)
        {
            var result = new StringBuilder();

            for (var index = startIndex; index < startIndex + length; index++)
            {
                result.Append(stringBuilder[index]);
            }

            return result;
        }

        public static StringBuilder Trim(this StringBuilder stringBuilder)
        {
            var valuesToTrim = new[] { '\b', '\n', '\r', '\t', '\v', ' '};

            var index = 0;
            while (index < stringBuilder.Length && valuesToTrim.Any(el => el == stringBuilder[index]))
            {
                index++;
            }

            if (index == stringBuilder.Length)
            {
                stringBuilder.Clear();
            }
            else
            {
                if (index < stringBuilder.Length && index > 0)
                {
                    stringBuilder.Remove(0, index);
                }

                index = stringBuilder.Length - 1;
                while (index > 0 && valuesToTrim.Any(el => el == stringBuilder[index]))
                {
                    index--;
                }

                if (index > 0 && index + 1 < stringBuilder.Length)
                {
                    stringBuilder.Remove(index + 1, stringBuilder.Length - index - 1);
                }
            }

            return stringBuilder;
        }

        public static StringBuilder Remove(this StringBuilder stringBuilder, char valueToRemove, bool ignoreCase = true)
        {
            var indexesToRemove = new List<int>();

            for (var index = 0; index < stringBuilder.Length; index++)
            {
                if (ignoreCase ? char.ToLower(stringBuilder[index]) == char.ToLower(valueToRemove) : stringBuilder[index] == valueToRemove)
                {
                    indexesToRemove.Add(index);
                }
            }

            for (var index = 0; index < indexesToRemove.Count; index++)
            {
                stringBuilder.Remove(indexesToRemove[index] - index, 1);
            }
            
            return stringBuilder;
        }

        public static StringBuilder TakeWhile(this StringBuilder stringBuilder, int startIndex, Func<char, bool> takeCondition)
        {
            var endIndex = startIndex;

            while (endIndex < stringBuilder.Length && takeCondition(stringBuilder[endIndex]))
            {
                endIndex++;
            }

            return endIndex < stringBuilder.Length && !takeCondition(stringBuilder[endIndex]) ? stringBuilder.GetRange(startIndex, endIndex - startIndex) : null;
        }
    }
}
