using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Configuration;
using System.Collections.Specialized;
using static System.Net.Mime.MediaTypeNames;
using SanwaMarco.Comm;
using System.Xml;
using SanwaMarco.Controller;
using log4net;

namespace SanwaMarco
{
    public class Procedure
    {
        string[] cmds;
        string condition;
    }
    public class JobUtil
    {
        public Dictionary<string, string> localVarMap = new Dictionary<string, string>();
        public string marcoName;
        private static readonly ILog logger = LogManager.GetLogger(typeof(ComPortClient));

        public JobUtil(string marcoName, Dictionary<string, string> localVarMap)
        {
            this.localVarMap = localVarMap;
            this.marcoName = marcoName;
        }

        //internal void Send(DeviceController device, string msg)
        //{

        //    if (!device.Status.Equals("Connected"))//檢查連線
        //        Console.WriteLine(device.Name + " Send:" + "未連線!");
        //    else if (device.processState.Equals(DeviceController.PROCESS_STATE_INIT))
        //        Console.WriteLine(device.Name + " Send:" + "未初始化!");
        //    else if (!device.processState.Equals(DeviceController.PROCESS_STATE_IDLE))
        //        Console.WriteLine(device.Name + " Send:" + "設備忙碌中或者未就緒!");
        //    else
        //    {
        //        Console.WriteLine(device + " Send:" + msg);
        //        //device.sendCommand(msg);
        //        API api = new API(localVarMap);
        //        api.doWork(device);
        //    }
                
        //}

        public string function_name;
        public static Dictionary<string, Procedure> procedureMap = new Dictionary<string, Procedure>();
        public ArrayList comands = new ArrayList();
        public Boolean skipFlag = false;
        public Boolean isFinish = false;
        public Boolean isDebug = false;
        public const int LOG_LEVEL_NORMAL = 1;
        public const int LOG_LEVEL_INFO = 2;
        public const int LOG_LEVEL_DEBUG = 3;
        public int logMode = LOG_LEVEL_INFO;//LOG_LEVEL_DEBUG
        public string jobResult;
        public string jobData;
        string lastLine = "";
        //MessageReport msgReport = new MessageReport();

        //public void Connect()
        //{
            
        //}

        public void RunMarco()
        {
            jobResult = "RunMarco_Error";
            //localVarMap.Clear();//清除區域變數
            isFinish = false;
            string filePath = "marco\\" + marcoName + ".vb";

            //這樣才能讀入中文字元 System.Text.Encoding.GetEncoding(950)
            try
            {
                StreamReader sr = new StreamReader(filePath, System.Text.Encoding.UTF8); //或 System.Text.Encoding.Default
                comands.Clear();
                while (sr.Peek() >= 0)
                {
                    string line = sr.ReadLine().Trim();

                    //Step1: 過濾無效資料
                    if (line.Length <= 0)
                    {
                        continue;//空白行不處理
                    }
                    else if(line.StartsWith("'"))
                    {
                        continue;//註解不處理
                    }
                    else
                    {
                        int endIndex = line.IndexOf(";'") > 0 ? line.IndexOf(";'") + 1 : line.Length;//去掉指令行後方註解
                        if(line.StartsWith("IF")|| line.StartsWith("ELSEIF")|| line.StartsWith("WHILE"))
                            endIndex = line.IndexOf(")'") > 0 ? line.IndexOf(")'") + 1 : line.Length;//去掉指令行後方註解(IF,ELSEIF,WHILE)
                        else if (line.StartsWith("ELSE"))
                            endIndex = line.IndexOf("'") > 0 ? line.IndexOf("'") : line.Length;//去掉指令行後方註解(ELSE)
                        line = line.Substring(0, endIndex);
                    }
                    comands.Add(line);//去掉頭尾空白
                }
                sr.Close();
                jobResult = parseMarco();
                //result = log.ToString();
            }
            catch (DirectoryNotFoundException de)
            {
                error(filePath + "路徑不存在.\n" + de.StackTrace + " " + de.Message);
                jobResult = filePath + "路徑不存在.\n" + de.StackTrace + " " + de.Message;
            }
            catch (FileNotFoundException fe )
            {
                error(filePath + "檔案不存在.\n" + fe.StackTrace + " " + fe.Message);
                jobResult = filePath + "檔案不存在.\n" + fe.StackTrace + " " + fe.Message;
            }
            catch (Exception e)
            {

                error(e.StackTrace + " " + e.Message);
                jobResult = e.StackTrace + " " + e.Message;
            }
            isFinish = true;
        }

        private string parseMarco()
        {
            string parseResult = "ParseMarco Error";
            try
            {
                for (int i = 0; i < comands.Count;)
                {
                    if (isFinish)
                        break;//命令中斷旗標已打開, 跳出 marco 處理
                    string cmd = (String)comands[i];
                    logger.Debug("parseMarco:" + cmd);
                    lastLine = cmd;//紀錄目前處理的 line
                    cmd = parseLine(cmd);//kuma

                    i++;
                    if (cmd.StartsWith("IF", StringComparison.InvariantCultureIgnoreCase))
                    {
                        // 判斷條件:成立的話 + 1 , 不成立時跳到下一個判斷式(ELSE,ELSEIF,ENDIF)
                        if (checkRule(cmd))
                        {
                            skipFlag = false;
                        }
                        else
                        {
                            skipFlag = true;
                            for (int j = i; j < comands.Count; j++)
                            {
                                string tmpCmd = (String)comands[j];
                                if (tmpCmd.StartsWith("ELSEIF", StringComparison.InvariantCultureIgnoreCase) ||
                                    tmpCmd.StartsWith("ELSE", StringComparison.InvariantCultureIgnoreCase) ||
                                    tmpCmd.StartsWith("ENDIF", StringComparison.InvariantCultureIgnoreCase))
                                {
                                    i = j;
                                    break;
                                }
                            }
                        }
                    }
                    //End IF
                    else if (cmd.StartsWith("ELSEIF", StringComparison.InvariantCultureIgnoreCase))
                    {
                        if (skipFlag == false)//如果上方的 IF 或 ELSEIF 做過的話, 直接跳到 ENDIF
                        {
                            for (int j = i; j < comands.Count; j++)
                            {
                                string tmpCmd = (String)comands[j];
                                if (tmpCmd.StartsWith("ENDIF", StringComparison.InvariantCultureIgnoreCase))
                                {
                                    i = j;
                                    break;
                                }
                            }
                        }
                        else
                        {
                            if (checkRule(cmd))
                            {
                                skipFlag = false;
                            }
                            else
                            {
                                skipFlag = true;
                                for (int j = i; j < comands.Count; j++)
                                {
                                    string tmpCmd = (String)comands[j];
                                    if (tmpCmd.StartsWith("ELSEIF", StringComparison.InvariantCultureIgnoreCase) ||
                                        tmpCmd.StartsWith("ELSE", StringComparison.InvariantCultureIgnoreCase) ||
                                        tmpCmd.StartsWith("ENDIF", StringComparison.InvariantCultureIgnoreCase))
                                    {
                                        i = j;
                                        break;
                                    }
                                }
                            }
                        }
                    }
                    //End ELSEIF
                    else if (cmd.StartsWith("ELSE", StringComparison.InvariantCultureIgnoreCase))
                    {
                        if (skipFlag == false)//如果上方的 IF 或 ELSE 做過的話, 直接跳到 ENDIF
                        {
                            for (int j = i; j < comands.Count; j++)
                            {
                                string tmpCmd = (String)comands[j];
                                if (tmpCmd.StartsWith("ENDIF", StringComparison.InvariantCultureIgnoreCase))
                                {
                                    i = j;
                                    break;
                                }
                            }
                        }
                        else
                        {
                            skipFlag = false;
                        }
                    }
                    //End ELSE
                    else if (cmd.StartsWith("ENDIF", StringComparison.InvariantCultureIgnoreCase))
                    {
                        skipFlag = false;//離開 IF 判斷式, 後續指令不 bypass
                    }
                    //End ENDIF
                    else if (cmd.StartsWith("WHILE", StringComparison.InvariantCultureIgnoreCase))
                    {
                        // 判斷條件:成立的話 + 1 , 不成立時跳到 ENDWHILE 
                        if (checkRule(cmd))
                        {
                            skipFlag = false;
                        }
                        else
                        {
                            skipFlag = true;
                            for (int j = i; j < comands.Count; j++)
                            {
                                string tmpCmd = (String)comands[j];
                                if (tmpCmd.StartsWith("ENDWHILE", StringComparison.InvariantCultureIgnoreCase))
                                {
                                    i = j;
                                    break;
                                }
                            }
                        }
                    }
                    //End WHILE
                    else if (cmd.StartsWith("ENDWHILE", StringComparison.InvariantCultureIgnoreCase))
                    {
                        if (skipFlag == true)// WHILE 條件式不成立
                        {
                            skipFlag = false;
                        }
                        else
                        {
                            for (int j = i; j > 0; j--)
                            {
                                string tmpCmd = (String)comands[j];
                                if (tmpCmd.StartsWith("WHILE", StringComparison.InvariantCultureIgnoreCase))
                                {
                                    i = j;
                                    break;
                                }
                            }
                        }
                    }
                    //End ENDWHILE
                    else
                    {
                        if (skipFlag == false)
                        {
                            string lineResult = ProcMarco(cmd);
                            if (!lineResult.Equals(""))
                            {
                                return lineResult;//有錯誤訊息，不繼續執行
                            }
                        }
                    }
                    //END ELSE

                }
                parseResult = "";
            }
            catch (Exception e)
            {
                error(e.StackTrace + " " + e.Message);
                parseResult = e.StackTrace;
            }
            return parseResult;
        }

        private bool checkRule(string rule)
        {
            Boolean result = false;
            string pattern = @"(ELSE)?IF *\(|WHILE *\(|\) *$";// 取代 "ELSEIF  (" , "IF  (" , "WHILE  ("  , 以及最後一個 ")"
            //string exp = cmd.Replace("ELSEIF", "").Replace("IF", "").Replace("WHILE", "").Replace(");", "").Replace("(", "").Trim();
            string temp = Regex.Replace(rule, pattern, "", RegexOptions.IgnoreCase).Trim();
            temp = trimDoubleQuotes(temp);  
            string exp = temp;//kuma
            string compute = new DataTable().Compute(exp, null).ToString();
            if (compute.ToLower().Equals("true"))
                result = true;
            return result;
        }

        public string ProcMarco(String line)
        {
            String result = "";
            //Step2: 優先處理 DECODE,CONCAT,ADD 指令
            if (line.Trim().Contains("DECODE("))
            {
                int funIdx = line.IndexOf("DECODE(");
                int endIdx = line.IndexOf(")", funIdx);
                int expLen = endIdx - funIdx + 1;//index 從0開始, 要加1
                string exp = line.Substring(funIdx, expLen);
                //result.Append("↑Call[DECODE] Function: " + decodeExp + "\n");
                line = line.Replace(exp, getDecode(exp));
            }
            if (line.Trim().Contains("CONCAT("))
            {
                int funIdx = line.IndexOf("CONCAT(");
                int endIdx = line.IndexOf(")", funIdx);
                int expLen = endIdx - funIdx + 1;//index 從0開始, 要加1
                string exp = line.Substring(funIdx, expLen);
                line = line.Replace(exp, getConcat(exp));
            }
            if (line.Trim().Contains("ADD("))
            {
                int funIdx = line.IndexOf("ADD(");
                int endIdx = line.IndexOf(")", funIdx);
                int expLen = endIdx - funIdx + 1;//index 從0開始, 要加1
                string exp = line.Substring(funIdx, expLen);
                line = line.Replace(exp, getAdd(exp));
            }
            //Step3: 處理 marco 指令
            debug(line + "\n");
            if (line.Trim().StartsWith("Function"))
            {
                //debug("↑宣告function 開始\n");//debug
            }
            else if (line.Trim().StartsWith("End Function;"))
            {
                //debug("↑宣告function 結束\n");//debug
            }
            else if (line.Trim().StartsWith("RETVAL("))
            {
                string _return = parseReturnVal(line.Trim());
                if (_return != null)
                {
                    isFinish = true;//return 條件成立, 之後不做了
                    info(_return);
                    result = _return;
                }
            }
            else if (line.Trim().StartsWith("RETURN(")|| line.Trim().StartsWith("Return ("))
            {
                string _return = parseReturn(line.Trim());
                if (_return != null)
                {
                    isFinish = true;//return 條件成立, 之後不做了
                    info(_return);
                    result = _return;
                }
            }
            else if (line.Trim().StartsWith("SETVAR("))
            {
                procSetVar(line.Trim());
            }
            else if (line.Trim().StartsWith("SETVARS("))
            {
                procSetVars(line.Trim());
            }
            else if (line.Trim().StartsWith("PRINT("))
            {
                //result.Append(parsePrint(line.Trim()));
                procPrint(line.Trim());
            }
            else if (line.Trim().StartsWith("DELAY("))
            {
                //result.Append(procDelay(line.Trim()));
                procDelay(line.Trim());
            }
            else if (line.Trim().StartsWith("API("))
            {
                //result.Append(line + "\n");//debug 用
                result = procAPI(line.Trim());
                if (!result.Equals(""))
                    isFinish = true;//return 條件成立, 之後不做了
            }
            else
            {
                return line.Substring(0, line.IndexOf(";")) + " parse error";
            }
            return result;
        }

        private void info(string msg)
        {
            if (logMode >= LOG_LEVEL_INFO)
            {
                logger.Info(msg);
            }
        }
        private void warring(string msg)
        {
            if (logMode >= LOG_LEVEL_NORMAL)
            {
                logger.Warn(msg);
            }
        }
        private void debug(string msg)
        {
            if (logMode >= LOG_LEVEL_DEBUG)
            {
                logger.Debug(msg);
            }
        }
        private void error(string msg)
        {
            msg = msg + "\nError Line:" + lastLine;
            if (logMode >= LOG_LEVEL_NORMAL)
            {
                logger.Error(msg);
            }
        }
        private string parseLine(string line)
        {
            Regex rx = new Regex("@<\\s*\\w*\\s*>|\\$<\\s*\\w*\\s*>", RegexOptions.Compiled | RegexOptions.IgnoreCase);
            MatchCollection matches = rx.Matches(line);
            foreach (Match match in matches)
            {
                String group;
                //System.out.println("matcher.group():\t" + group);
                String replacement = "";
                String varName = "";
                group = match.Groups[0].Value;
                varName = group.Substring(2, group.Length - 3).Trim();
                if (group.StartsWith("@"))//區域變數
                {
                    localVarMap.TryGetValue( "@" + varName, out replacement);
                }
                else if (group.StartsWith("$"))//全域變數
                {
                    Marco.pubVarMap.TryGetValue( "$" + varName, out replacement);
                }
                replacement = replacement != null ? replacement : "undefined";
                line = line.Replace(group, replacement);
            }
            return line;
        }

        private string getVar1(string key)
        {
            string result = null;
            key = key.Trim();//去除頭部空白

            if (key.Contains("+"))
            {
                string[] msgs = key.Split(new char[] { '+' }, StringSplitOptions.RemoveEmptyEntries);
                foreach (string foo in msgs)
                {
                    //result = (result != null? result:"") + getVar(foo);//kuma
                    result = (result != null ? result : "") + foo;
                }
            }
            else if (key.StartsWith("@"))//區域變數
            {
                //key = key.TrimEnd();//去除頭尾空白
                localVarMap.TryGetValue(key, out result);
            }
            else if (key.StartsWith("$"))//全域變數
            {
                //key = key.TrimEnd();//去除頭尾空
                Marco.pubVarMap.TryGetValue(key,out result);
            }
            else if(key.StartsWith("\"") && key.EndsWith("\""))//固定字串
            {
                result = key.Replace("\"", "");
            }

            return result != null? result: "undefined";
        }

        private void setVar(string key, string value)
        {
            if (key.StartsWith("@"))//區域變數
            {
                localVarMap[key] = value;
            }
            else if (key.StartsWith("$"))//全域變數
            {
                Marco.pubVarMap[key] = value;
            }
        }

        
        private string getConcat(string exp)
        {

            string val = "";
            //string[] tokens = exp.Replace("CONCAT", "").Split(new char[] { '(', ',', ')' }, StringSplitOptions.RemoveEmptyEntries);
            Regex rx = new Regex("(?:^|,)(\\\"(?:[^\\\"]+|\\\"\\\")*\\\"|[^,]*)", RegexOptions.Compiled | RegexOptions.IgnoreCase);
            //string switchItem1 = getVar(tokens[0].Replace("\"", ""));
            MatchCollection matches = rx.Matches(Regex.Replace(exp, "^CONCAT\\(|\\)$",""));
            foreach (Match match in matches)
            {
                string temp = "";
                if (match.Groups[1].Value.Length > 0)
                    temp = match.Groups[1].Value;
                else
                    temp = match.Groups[2].Value;
                temp = trimDoubleQuotes(temp);
                //val = val + getVar(temp);
                val = val + temp;//kuma
            }

            //for (int i = 0; i < tokens.Length; i++)
            //{
            //    tokens[i] = Regex.Replace(tokens[i], @"^""|""$", "").Trim();// 去掉頭尾的 "
            //    val = val + getVar(tokens[i]);
            //}
            //return val;
            return "\"" + val + "\"";
        }

        private string getAdd(string decodeExp)
        {
            string[] tokens = decodeExp.Replace("ADD", "").Split(new char[] { '(', ',', ')' }, StringSplitOptions.RemoveEmptyEntries);
            //string switchItem = getVar(tokens[0].Replace("\"", ""));//kuma
            string switchItem = tokens[0].Replace("\"", "");

            int val = 0;
            for (int i = 0; i < tokens.Length; i++)
            {
                int tmp;
                tokens[i] = trimDoubleQuotes(tokens[i]);
                //if (int.TryParse(getVar(tokens[i]), out tmp))//kuma
                if (int.TryParse(tokens[i], out tmp))
                        val = val + tmp;
                else
                    return "-1";
            }
            return val.ToString();
        }

        private string getDecode(string decodeExp)
        {
            string[] tokens = decodeExp.Replace("DECODE", "").Split(new char[] { '(', ',', ')' }, StringSplitOptions.RemoveEmptyEntries);
            //string switchItem = getVar(tokens[0].Replace("\"",""));//kuma
            string switchItem = tokens[0].Trim().Replace("\"", "");

            string val = null;//預設值
            string defVal = "undefined";//預設值
            if (tokens.Length % 2 == 0)
            {
                defVal = tokens[tokens.Length - 1].Trim().Replace("\"", "");
            }
            for(int i=1; i< tokens.Length; i = i+2)
            {
                string caseItem = tokens[i].Trim().Replace("\"","");
                if (switchItem.Equals(caseItem))
                {
                    val = tokens[i+1].Trim().Replace("\"", "");
                    break;
                }
            }
            if(val == null)
            {
                val = defVal;
            }
            return "\"" + val + "\"";
        }

        private Boolean procSetVars(string func)
        {
            Boolean result = false;
            try
            {
                // VS 工具會很雞婆的把格式做調整，如下
                // SETVAR("@cmd","$1CMD:GETW_:1202,10,1"); => SETVAR("@cmd", "$1CMD:GETW_:1202,10,1");
                // 所以新增.Replace("\", \"", "\",\"");
                func = func.Replace("SETVARS(", "").Replace(");", "").Replace("\", \"", "\",\"");
                RegexOptions options = ((RegexOptions.IgnorePatternWhitespace | RegexOptions.Multiline) | RegexOptions.IgnoreCase);

                Regex reg = new Regex("(?<=^|,)(\"(?:[^\"]|\"\")*\"|[^,]*)", options);
                MatchCollection coll = reg.Matches(func);
                for (int i = 0; i < coll.Count; i = i + 2)
                {
                    string key = coll[i].Value.Trim().Replace("\"", "");
                    string value = coll[i + 1].Value.Trim().Replace("\"", "");
                    setVar(key, value);
                    debug("Call [SETVARS]:" + " Arg1:" + key + ", Arg2:" + value + "\n");//debug
                }
                result = true;
            }
            catch (Exception e)
            {
                error(e.StackTrace + " " + e.Message);
            }
            return result;
        }

        private Boolean procSetVar(string func)
        {
            Boolean result = false;
            try
            {
                // VS 工具會很雞婆的把格式做調整，如下
                // SETVAR("@cmd","$1CMD:GETW_:1202,10,1"); => SETVAR("@cmd", "$1CMD:GETW_:1202,10,1");
                // 所以新增.Replace("\", \"", "\",\"");
                func = func.Replace("SETVAR(", "").Replace(");", "").Replace("\", \"", "\",\"");
                string arg1 = "";
                string arg2 = "";
                RegexOptions options = ((RegexOptions.IgnorePatternWhitespace | RegexOptions.Multiline) | RegexOptions.IgnoreCase);

                Regex reg = new Regex("(?<=^|,)(\"(?:[^\"]|\"\")*\"|[^,]*)", options);
                MatchCollection coll = reg.Matches(func);
                string[] items = new string[coll.Count];
                arg1 = coll[0].Value.Trim().Replace("\"", "");
                arg2 = coll[1].Value.Trim().Replace("\"", "");
                string value = arg2;
                setVar(arg1, value);
                debug("Call [SETVAR]:" + " Arg1:" + arg1 + ", Arg2:" + arg2 + "\n");//debug
                result = true;
            }
            catch (Exception e)
            {
                error(e.StackTrace + " " + e.Message);
            }
            return result;
        }       
        
        private string procAPI(string func)
        {
            string methodName = "";
            Boolean autoReturn = true;// default 當發生異常或有回傳值時，會返回前台
            string errorCode = "";
            string returnValue = "";
            String result = "";
            try
            {
                func = func.Replace("API(", "").Replace(");", "");
                string[] args = func.Split(',');
                methodName = args[0].Trim().Replace("\"", "");
                autoReturn = args[1].Trim().Replace("\"", "").ToLower().Equals("true") ? true : false;
                // 未指令 return code 時, 會使用 API 的預設 return code, 存在 method_result
                errorCode = args.Length >= 3 ? args[2].Trim().Replace("\"", "") : "@" + methodName + "_RESULT";
                API api = new API(methodName, autoReturn, errorCode, localVarMap);//建構API
                result = api.Run(ref returnValue); //呼叫API
            }
            catch (Exception e)
            {
                logger.Error("procAPI:" + func + " error." + e.StackTrace);
            }
            if(!errorCode.Equals(""))
                localVarMap[errorCode] = jobResult;//set result
            if (!methodName.Equals(""))
            {
                this.jobData = autoReturn ? returnValue : "";// 當autoReturn 為 false 發生異常或有回傳值時，不會返回前台
                localVarMap["@" + methodName + "_RETURN"] = returnValue;//set return value
            }
            result = autoReturn ? result : "";// 當autoReturn 為 false 發生異常或有回傳值時，不會返回前台
            return result;
        }
        private Boolean procDelay(string func)
        {
            Boolean result = false;
            try
            {
                string time = func.Replace("DELAY(", "").Replace(");", "");
                int delay = 0;
                int.TryParse(time, out delay);
                if (delay != 0)
                {
                    Thread.Sleep(delay);
                }
                else
                {
                    error("DELAY Time format error:" + time + "\n");//debug
                }
            }
            catch (Exception e)
            {
                error(e.StackTrace + " " + e.Message);
            }
            return result;
        }
        private string trimDoubleQuotes(string text)
        {
            return Regex.Replace(text, @"^ *""?|""? *$", "");// 去掉頭尾的 " 與空白
        }
        //private string parsePrint(string exp)
        //{
        //    StringBuilder result = new StringBuilder();
        //    //kuma 之後要改成送資料到HOST
        //    result.Append("********** Print ********** " + getVar(exp.Replace("PRINT(", "").Replace(");", "")) + "\n");
        //    return result.ToString();
        //}
        private Boolean procPrint(string exp)
        {
            Boolean result = false;
            try
            {
                //string msg = "********** Print ********** " + getVar(exp.Replace("PRINT(", "").Replace(");", "")) + "\n";//kuma
                exp = exp.Replace("PRINT(", "").Replace(");", "");
                exp = trimDoubleQuotes(exp);
                string msg = null;
                if (exp.Contains("+"))
                {
                    string[] msgs = exp.Split(new char[] { '+' }, StringSplitOptions.RemoveEmptyEntries);
                    foreach (string foo in msgs)
                    {
                        //result = (result != null? result:"") + getVar(foo);//kuma
                        string temp = trimDoubleQuotes(foo);
                        msg = (msg != null ? msg : "") + temp;
                    }
                    //msg = msg + "\n";
                }
                else
                {
                    //msg = exp + "\n";
                    msg = exp;
                }
                info(msg);
                Marco.Print(msg);
                result = true;
            }
            catch(Exception e)
            {
                error(e.StackTrace + " " + e.Message);
                result = false;
            }
            return result;
        }

        
        private string parseReturnVal(string func)
        {
            StringBuilder result = new StringBuilder();
            func = func.Replace("RETVAL(", "").Replace(");", "").Trim();
            string data = "";
            RegexOptions options = ((RegexOptions.IgnorePatternWhitespace | RegexOptions.Multiline) | RegexOptions.IgnoreCase);

            Regex reg = new Regex("(?<=^|,)(\"(?:[^\"]|\"\")*\"|[^,]*)", options);
            //Regex reg = new Regex("(?:^|,)(\\\"(?:[^\\\"]+|\\\"\\\")*\\\"|[^,]*)", options);
            MatchCollection coll = reg.Matches(func);
            string[] items = new string[coll.Count];
            //arg1 = getVar(coll[0].Value);//kuma
            data = coll[0].Value;
            data = trimDoubleQuotes(data);
            this.jobData = data;
            return "";
        }
        private string parseReturn(string func)
        {
            StringBuilder result = new StringBuilder();
            func = func.Replace("RETURN(", "").Replace("Return (", "").Replace(");","").Trim();
            string arg1 = "";
            string arg2 = "";
            RegexOptions options = ((RegexOptions.IgnorePatternWhitespace | RegexOptions.Multiline) | RegexOptions.IgnoreCase);
            
            Regex reg = new Regex("(?<=^|,)(\"(?:[^\"]|\"\")*\"|[^,]*)", options);
            //Regex reg = new Regex("(?:^|,)(\\\"(?:[^\\\"]+|\\\"\\\")*\\\"|[^,]*)", options);
            MatchCollection coll = reg.Matches(func);
            string[] items = new string[coll.Count];
            //arg1 = getVar(coll[0].Value);//kuma
            arg1 = coll[0].Value;
            arg1 = trimDoubleQuotes(arg1);
            if (coll.Count >= 2)
            {
                arg2 = coll[1].Value;
                arg2 = Regex.Replace(arg2, @"^,""|""$", "").Trim(); ;
                //Regex.Replace(temp, @"^""|""$", "").Trim();// 去掉頭尾的 "
            }
            else
            {
                arg2 = "true";//沒有參數2時，條件式永遠成立
            }
            if (checkRule("IF(" + arg2 + ")") == true)
                return arg1;
            else
                return null;//條件式不成立, 不須結束程式
        }

        //private string RETURN(string msg, string exp)
        //{
        //    return "Call [RETURN]:" + "\nArg1:" + msg + "\nArg2:" + exp + "\n";//debug
        //}

        private string processCode(string line)
        {
            StringBuilder result = new StringBuilder();
            string[] foos = line.Trim().Split(new char[] { '(', ')', ',' }, StringSplitOptions.RemoveEmptyEntries);
            if (foos.Length > 0)
            {
                foreach (string foo in foos)
                {
                    if(foo.Length > 0)
                        result.Append(foo + "\n");
                }
            }
            else
            {
                result.Append("foos.Length is 0!\n");
            }
            return result.ToString();
        }
    }
}
