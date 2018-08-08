/**
 * Copyright 2017-2018 InterlockLedger Federation
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *     http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace add_copyright
{
    public static class StringExtensions
    {
        public static bool IsOneOf(this string item, params string[] list) => list.Any(s => item.Equals(s, StringComparison.OrdinalIgnoreCase));
    }

    internal class Program
    {
        private static readonly Regex _regexOfPreviousHeader = new Regex(@"\/\*+.+Copyright.+\*\/", RegexOptions.IgnoreCase | RegexOptions.Singleline);

        private static void AddTemplatedCopyrightHeader(string directory, string template)
        {
            if (Path.GetFileName(directory).IsOneOf("obj", "bin", ".vs"))
                return;
            foreach (var file in Directory.EnumerateFiles(directory, "*.cs"))
                PrependHeader(template, file);
            foreach (var subdir in Directory.EnumerateDirectories(directory))
                AddTemplatedCopyrightHeader(subdir, template);
        }

        private static string GetTemplate(string headerTemplateFilePath)
        {
            if (!Path.IsPathRooted(headerTemplateFilePath))
                headerTemplateFilePath = Path.GetFullPath(headerTemplateFilePath);
            if (!File.Exists(headerTemplateFilePath))
                throw new Exception($"Can't find template file '{headerTemplateFilePath}'");
            return File.ReadAllText(headerTemplateFilePath).Replace("@year@", DateTime.Now.Year.ToString());
        }

        private static void Main(string[] args)
        {
            AddTemplatedCopyrightHeader(
                directory: args.FirstOrDefault() ?? Directory.GetCurrentDirectory(),
                template: GetTemplate(args.Skip(1).FirstOrDefault() ?? "copyright.template"));
        }

        private static void PrependHeader(string template, string file)
        {
            Console.WriteLine(file);
            var originalText = File.ReadAllText(file);
            var newText = originalText;
            if (_regexOfPreviousHeader.IsMatch(originalText))
                newText = _regexOfPreviousHeader.Replace(originalText, template);
            else
                newText = template + Environment.NewLine + Environment.NewLine + originalText;
            File.WriteAllText(file, newText);
        }
    }
}