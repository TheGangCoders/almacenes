using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace WS_IT.DTOs
{
    public class DGrupoSeguridadDTO
    {
        public int IdDGrupoSeguridad { get; set; }
        public int IdGrupoSeguridad { get; set; }
        public bool DiferenteLogin { get; set; }
        public bool RepetirClave { get; set; }
        public int RepetirClaveDesde { get; set; }
        public int DiasCambioClave { get; set; }
        public bool Anulado { get; set; }
        public int RegexClaveCaracteresMinimo { get; set; }
        public int RegexClaveCaracteresMaximo { get; set; }
        public bool RegexClaveCaracterEspecial { get; set; }
        public bool RegexClaveMayuscula { get; set; }
        public bool RegexClaveMinuscula { get; set; }
        public bool RegexClaveNumero { get; set; }

        public string RegexMessage { get; set; }
        public string RegexPattern { get; set; }

        private static List<string> symbols = new List<string>()
        {
            "!", "#", "$", "%", "&", "(", ")", "=", "?", "¡", "_", "-", "[", "]", "{", "}", "*", "'", "¿",
            "+", ",", ".", ";", ":", "<", ">"
        };


        public void LoadRegex()
        {
            int lengthMinRequired = 6;
            int lengthMin = RegexClaveCaracteresMinimo > lengthMinRequired ? RegexClaveCaracteresMinimo : lengthMinRequired;
            int lengthMax = RegexClaveCaracteresMaximo > lengthMinRequired ? RegexClaveCaracteresMaximo : lengthMinRequired;
            lengthMax = lengthMax > lengthMin ? lengthMax : lengthMin;

            RegexPattern = "^";
            RegexMessage = "No cumple con requisitos de dificultad(De " + lengthMin + " a " + lengthMax +" caracteres";
            if (RegexClaveCaracterEspecial)
            {
                RegexPattern += "(?=.*[";
                for (int i = 0; i < symbols.Count; i++)
                {
                    if (symbols[i] == "(" ||
                        symbols[i] == ")" ||
                        symbols[i] == "{" ||
                        symbols[i] == "}" ||
                        symbols[i] == "[" ||
                        symbols[i] == "]" ||
                        symbols[i] == "-")
                    {
                        RegexPattern += "\\";
                    }
                    RegexPattern += symbols[i];
                }
                RegexPattern += "])";
                RegexMessage += ", un caracter especial";
            }
            if (RegexClaveMinuscula)
            {
                RegexPattern += "(?=.*[a-z])";
                RegexMessage += ", un letra minúscula";
            }
            if (RegexClaveMayuscula)
            {
                RegexPattern += "(?=.*[A-Z])";
                RegexMessage += ", un letra mayúscula";
            }
            if (RegexClaveNumero)
            {
                RegexPattern += "(?=.*[0-9])";
                RegexMessage += ", un número";
            }
            RegexPattern += "\\S{" + lengthMin + "," + lengthMax + "}$";
            RegexMessage += ")";

        }

        public ResponseSimpleDTO Match(string password)
        {
            ResponseSimpleDTO responseSimple = new ResponseSimpleDTO();
            Regex regex = new Regex(RegexPattern);
            responseSimple.Ok = regex.IsMatch(password);
            if(responseSimple.Ok)
            {
                responseSimple.Message = "Validado correctamente";
            } else
            {
                responseSimple.Message = RegexMessage;
            }
            return responseSimple;
        }
        public string GenerateRandomPassword()
        {
            int lengthMinRequired = 6;
            Random random = new Random();
            //int lengthMin = RegexClaveCaracteresMinimo > lengthMinRequired ? RegexClaveCaracteresMinimo : lengthMinRequired;
            int lengthMax = RegexClaveCaracteresMaximo > lengthMinRequired ? RegexClaveCaracteresMaximo : lengthMinRequired;
            //lengthMax = lengthMax > lengthMin ? lengthMax : lengthMin;
            //int lengthRandom = random.Next(lengthMin, lengthMax);

            int lengthRandom = lengthMax;

            List<string> passwordCharacters = new List<string>();

            for(int i = 0; i < lengthRandom; i++)
            {
                int typeCharacter = random.Next(0, 3);
                if (i <= 3)
                {
                    if (i == 0 && RegexClaveCaracterEspecial) { typeCharacter = 0; };
                    if (i == 1 && RegexClaveMinuscula) { typeCharacter = 1; };
                    if (i == 2 && RegexClaveMayuscula) { typeCharacter = 2; };
                    if (i == 3 && RegexClaveNumero) { typeCharacter = 3; };
                }

                switch(typeCharacter)
                {
                    case 0:
                        passwordCharacters.Add(symbols[random.Next(0, symbols.Count() - 1)]);
                        break;
                    case 1:
                        passwordCharacters.Add(Char.ToString((char)random.Next(97, 122))); //a-z
                        break;
                    case 2:
                        passwordCharacters.Add(Char.ToString((char)random.Next(65, 90))); //A-Z
                        break;
                    case 3:
                        passwordCharacters.Add(Char.ToString((char)random.Next(48, 57))); //0-9
                        break;
                    default:
                        break;
                }
            }

            passwordCharacters.Sort(new Comparison<string>((s1, s2) => random.Next(-1, 1)));

            return String.Join("", passwordCharacters);
        }
    }
}