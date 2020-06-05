using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scanner
{


    public class Scanner
    {
        public static int leftBracketsCount = 0;
        public static int rightBracketsCount = 0;
        public static int leftBracesCount = 0;
        public static int rightBracesCount = 0;
        public static int quotationsCount = 0;
        public static int illegalflag = 0;
        public static int numberflag = 0;
        public static int stringflag = 0;
        public enum RESERVED_WORDS
        {
            T_INT, T_FLOAT, T_STRING, T_IF, T_THEN, T_ELSE, T_ELSEIF, T_END, T_REPEAT,
            T_UNTIL, T_READ, T_WRITE, T_RETURN, T_ENDL
        };
        public enum SPECIAL_SYMBOLS
        {
            T_PLUS, T_MINUS, T_MULTIPLY, T_DIVIDE, T_EQUALTO, T_LESSTHAN, T_GREATERTHAN, T_LEFTBRACKET,
            T_RIGHTBRACKET, T_LEFTBRACE, T_RIGHTBRACE, T_ARROR, T_ARRAND, T_SEMICOLON, T_QUOTE
        };
        public enum STATES
        {
            START, T_NUMBER, T_IDENTIFIER, T_ASSIGN, T_NOTEQUALTO, T_STARTCOMMENT, T_INCOMMENT, T_ENDCOMMENT, T_STR,
            T_LOGOR, T_LOGAND,T_UNACCEPTEDNUMBER, SPECIALSYMBOLS, ILLEGALCHAR, ACCEPT
        };


        List<Token> tokensList = new List<Token>();

        public List<Token> getListOfTokens(string MyCode)
        {
            MyCode = MyCode + "  ";
            string state = STATES.START.ToString();
            string lastState = STATES.START.ToString();
            string currentTokenValue = "";

            int i = 0;

            while (true)
            {

                if (i > MyCode.Length - 1) break; //case end of my code is reached
                char character = MyCode[i];
                switch (state)
                {
                    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                    case "START":
                        if (char.IsWhiteSpace(character))
                        {
                            state = STATES.START.ToString();
                        }
                        else if (char.IsDigit(character))
                        {
                            state = STATES.T_NUMBER.ToString();
                            currentTokenValue += character;

                        }
                        else if (char.IsLetter(character))
                        {
                            state = STATES.T_IDENTIFIER.ToString();
                            currentTokenValue += character;
                        }
                        else if (character == ':')
                        {
                           
                            state = STATES.T_ASSIGN.ToString();
                            currentTokenValue += character;
                        }
                        else if (character == '<')
                        {
                           
                            state = STATES.T_NOTEQUALTO.ToString();
                            currentTokenValue += character;
                        }
                        else if (character == '/')
                        {
                            
                            state = STATES.T_STARTCOMMENT.ToString();
                            currentTokenValue += character;
                        }

                        else if (character == '"')
                        {
                            
                            state = STATES.T_STR.ToString();
                            currentTokenValue += character;
                            stringflag = 1;
                        }
                        else if (character == '+' || character == '-' || character == '(' ||
                                 character == '=' || character == ')' || character == '>' ||
                                 character == '{' || character == '}' || character == ';' ||
                                 character == '*')

                        {
                            
                            currentTokenValue += character;
                            state = STATES.SPECIALSYMBOLS.ToString();
                        }
                        else if (character == '|')
                        {
                           
                            state = STATES.T_LOGOR.ToString();
                            currentTokenValue += character;
                        }
                        else if (character == '&')
                        {
                           
                            state = STATES.T_LOGAND.ToString();
                            currentTokenValue += character;
                        }
                        else
                        {
                            illegalflag++;
                            state = STATES.ILLEGALCHAR.ToString();

                        }
                        lastState = state;
                        break;
                    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                    case "T_NUMBER":
                        if (char.IsDigit(character))
                        {

                            currentTokenValue += character;
                            state = STATES.T_NUMBER.ToString();
                        }
                        else if(char.IsLetter(character))
                        {
                            currentTokenValue += character;
                            numberflag++;
                            i++;
                            state = STATES.T_UNACCEPTEDNUMBER.ToString();
                        }
                        else
                        {
                            i++;
                            state = STATES.ACCEPT.ToString();
                        }

                        break;
                    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                    case "T_UNACCEPTEDNUMBER":

                            i++;
                            currentTokenValue += character;
                            state = STATES.ACCEPT.ToString();                   

                        break;
                    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                    case "T_IDENTIFIER":
                        if (((char.IsLetter(character)) || (char.IsDigit(character))) && (!(char.IsWhiteSpace(character))))
                        {

                            currentTokenValue += character;
                            state = STATES.T_IDENTIFIER.ToString();
                        }
                        else
                        {
                            state = STATES.ACCEPT.ToString();
                        }
                        break;

                    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                    case "T_NOTEQUALTO":
                        if (character == '>')
                        {

                            currentTokenValue += character;
                            i++;
                            state = STATES.ACCEPT.ToString();

                        }
                        else
                        {
                            i--;
                            currentTokenValue = "<";
                            state = STATES.SPECIALSYMBOLS.ToString();
                            lastState = state;
                        }
                        break;
                    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                    case "T_ASSIGN":
                        if (character == '=')
                        {

                            currentTokenValue += character;
                            i++;
                            state = STATES.ACCEPT.ToString();

                        }
                        else
                        {
                            i--;
                            currentTokenValue = ":";
                            state = STATES.SPECIALSYMBOLS.ToString();
                            lastState = state;
                        }
                        break;
                    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                    case "T_LOGOR":
                         if (character == '|')
                        {
                            currentTokenValue += character;
                            i++;
                            state = STATES.ACCEPT.ToString();
                        }
                        else
                        {

                            i--;
                            currentTokenValue = "|";
                            state = STATES.SPECIALSYMBOLS.ToString();
                            lastState = state;
                        }
                        break;
                    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                    case "T_LOGAND":
                        if (character == '&')
                        {
                            currentTokenValue += character;
                            i++;
                            state = STATES.ACCEPT.ToString();
                        }
                        else
                        {

                            i--;
                            currentTokenValue = "&";
                            state = STATES.SPECIALSYMBOLS.ToString();
                            lastState = state;
                        }
                        break;
                    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                    case "ILLEGALCHAR":

                        i++;
                        state = STATES.START.ToString();


                        break;
                    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                    case "SPECIALSYMBOLS":

                        i++;
                        state = STATES.ACCEPT.ToString();

                        break;
                    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                    case "T_STARTCOMMENT":

                        if (character == '*')
                        {

                            state = STATES.T_INCOMMENT.ToString();
                        }
                        else
                        {

                            i--;
                            currentTokenValue = "/";
                            state = STATES.SPECIALSYMBOLS.ToString();
                            lastState = state;
                        }

                        break;
                    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                    case "T_INCOMMENT":

                        if (character == '*')
                        {
                            state = STATES.T_ENDCOMMENT.ToString();
                        }
                        else
                        {

                            state = STATES.T_INCOMMENT.ToString();
                        }
                        break;
                    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                    case "T_ENDCOMMENT":
                        if (character == '/')
                        {
                            currentTokenValue = "";
                            state = STATES.START.ToString();
                        }
                        else
                        {
                            state = STATES.T_INCOMMENT.ToString();
                        }

                        break;

                    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                    case "T_STR":

                        if (character == '"')
                        {
                            currentTokenValue += character;
                            i++;
                            stringflag = 0;
                            state = STATES.ACCEPT.ToString();
                        }
                        else
                        {
                            currentTokenValue += character;
                            state = STATES.T_STR.ToString();
                        }
                        break;
                    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                    case "ACCEPT":

                        Token token = new Token();
                        if (lastState == "SPECIALSYMBOLS")
                        {
                            token.tokenValue = currentTokenValue;
                            token.TokenType = getSpecialSymbolsTokenType(currentTokenValue);

                        }
                      

                        else if (lastState == "T_IDENTIFIER")
                        {
                            if (currentTokenValue.Length > 30)
                            {
                                token.TokenType = getReservedWordsTokenType(currentTokenValue);
                                token.TokenType = "error: Identifier Exceeded limit";
                            }
                            else
                            {
                                token.tokenValue = currentTokenValue;
                                token.TokenType = getReservedWordsTokenType(currentTokenValue);
                            }
                        }
                        else
                        {
                            token.tokenValue = currentTokenValue;
                            token.TokenType = lastState;

                        }
                        if (illegalflag > 0 || stringflag > 0 || numberflag > 0)
                        {
                            currentTokenValue = "";
                            state = STATES.START.ToString();
                            lastState = STATES.ACCEPT.ToString();
                            illegalflag = numberflag = stringflag = 0;
                            break;
                        }
                        tokensList.Add(token);
                        currentTokenValue = "";
                        state = STATES.START.ToString();
                        lastState = STATES.ACCEPT.ToString();
                        break;
                        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                }
               
                if (state == "ACCEPT" || lastState == "ACCEPT") { }
                else
                {
                    
                    if (lastState == "SPECIALSYMBOLS") { }
                    else i++;
                }

            }

            Token endOfTokens = new Token();
            endOfTokens.TokenType = "ACCEPT";
            endOfTokens.tokenValue = "$";
            tokensList.Add(endOfTokens);
            return tokensList;
        }

        public string getSpecialSymbolsTokenType(string value)
        {

            if (value[0] == '+') { return SPECIAL_SYMBOLS.T_PLUS.ToString(); }
            else if (value[0] == '-') { return SPECIAL_SYMBOLS.T_MINUS.ToString(); }
            else if (value[0] == '*') { return SPECIAL_SYMBOLS.T_MULTIPLY.ToString(); }
            else if (value[0] == '/') { return SPECIAL_SYMBOLS.T_DIVIDE.ToString(); }
            else if (value[0] == '=') { return SPECIAL_SYMBOLS.T_EQUALTO.ToString(); }
            else if (value[0] == '<') { return SPECIAL_SYMBOLS.T_LESSTHAN.ToString(); }
            else if (value[0] == '>') { return SPECIAL_SYMBOLS.T_GREATERTHAN.ToString(); }
            else if (value[0] == '"') { return SPECIAL_SYMBOLS.T_QUOTE.ToString(); }
            else if (value[0] == ')')
            {
                rightBracketsCount++;
                return SPECIAL_SYMBOLS.T_RIGHTBRACKET.ToString();
            }
            else if (value[0] == '(')
            {
                leftBracketsCount++;
                return SPECIAL_SYMBOLS.T_LEFTBRACKET.ToString();
            }
            else if (value[0] == ';') { return SPECIAL_SYMBOLS.T_SEMICOLON.ToString(); }
            else if (value[0] == '{')
            {
                leftBracesCount++;
                return SPECIAL_SYMBOLS.T_RIGHTBRACE.ToString();
            }
            else if (value[0] == '}')
            {
                rightBracesCount++;
                return SPECIAL_SYMBOLS.T_LEFTBRACE.ToString();
            }
            else if (value[0] == '&') { return SPECIAL_SYMBOLS.T_ARRAND.ToString(); }
            else if (value[0] == '|') { return SPECIAL_SYMBOLS.T_ARROR.ToString(); }         
            else return null;

        }


        public string getReservedWordsTokenType(string value)
        {

            /* if (value == "if") { return RESERVED_WORDS.T_IF.ToString(); }
             else if (value == "then") { return RESERVED_WORDS.T_THEN.ToString(); }
             else if (value == "else") { return RESERVED_WORDS.T_ELSE.ToString(); }
             else if (value == "end") { return RESERVED_WORDS.T_END.ToString(); }
             else if (value == "repeat") { return RESERVED_WORDS.T_REPEAT.ToString(); }
             else if (value == "until") { return RESERVED_WORDS.T_UNTIL.ToString(); }
             else if (value == "read") { return RESERVED_WORDS.T_READ.ToString(); }
             else if (value == "write") { return RESERVED_WORDS.T_WRITE.ToString(); }
             else if (value == "int") { return RESERVED_WORDS.T_INT.ToString(); }
             else if (value == "float") { return RESERVED_WORDS.T_FLOAT.ToString(); }
             else if (value == "string") { return RESERVED_WORDS.T_STRING.ToString(); }
             else if (value == "endl") { return RESERVED_WORDS.T_ENDL.ToString(); }
             else if (value == "return") { return RESERVED_WORDS.T_STRING.ToString(); }
             else return value + ".ID";*/

            foreach (RESERVED_WORDS item in Enum.GetValues(typeof(RESERVED_WORDS)))
            {
                if (value == item.ToString().ToLower().Substring(2))
                {
                    return item.ToString();
                }
            }
            return "T_IDENTIFIER";
        }

        public int bracesMismatchErrorCheck()
        {
            if (leftBracesCount != rightBracesCount)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }

        public int bracketsMismatchErrorCheck()
        {
            if (leftBracketsCount != rightBracketsCount)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }

        public int quotationsMismatchErrorCheck()
        {
            if (stringflag == 1)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }

        public int isIllegal()
        {
            if (illegalflag >= 1)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }
        public int isNumberError()
        {
            if (numberflag >= 1)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }


    }
}


