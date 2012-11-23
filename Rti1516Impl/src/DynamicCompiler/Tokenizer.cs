using System;
using System.Collections.Generic;
using System.Text;

namespace Sxta.Rti1516.DynamicCompiler
{

    /// <summary>
    /// TODO: Be carefull. There is another tokenizer class in supportClass. 
    /// </summary>
    public class Tokenizer
    {
        private String[] tokens;
        private int currentToken;

        //The tokenizer uses the default delimiter set: the space character, the tab character, the newline character, and the carriage-return character and the form-feed character
        const string delimitersDefatult = " \t\n\r\f";
        private string delimiters;

        public Tokenizer(String source)
            : this(source, delimitersDefatult)
        {
        }

        public Tokenizer(String source, string dlmters)
        {
            delimiters = dlmters;
            this.tokens = source.Split(delimiters.ToCharArray(), System.StringSplitOptions.RemoveEmptyEntries);
            this.currentToken = 0;
        }


        public String NextToken()
        {
            if (!HasMoreTokens()) throw new System.ArgumentOutOfRangeException();
            else return tokens[currentToken++];
        }

        public Boolean HasMoreTokens()
        {
            return this.currentToken < this.tokens.Length;
        }

    }
}
