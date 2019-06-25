Function RobotSpeed() AS String;
	
    'SETVAR("@value","-50");'DEBUG:設定值測試
	'Check Value
	Return ("ID_EMPTY", "'@<id>' = 'undefined'");'SPEED 參數未定義(行結束符號與註解單引號間, 不要留白)
	Return ("ID_ERROR", "'@<id>' <> '12345'");'SPEED 參數未定義
	'Set Controller
 '   PRINT("@device:@<device>");'DEBUG:檢查值
	'SETVAR("@device", "Robot01");
 '   PRINT("@device:@<device>");'DEBUG:檢查值
 '   DELAY(100);
 '   PRINT("@id:@<id>");'DEBUG:檢查值
 '   DELAY(100);
 '   PRINT("@name:@<name>");'DEBUG:檢查值
 '   DELAY(100);
	'SETVAR("@cmd", "$1CMD:GETW_:1202,10,1");'$1CMD:GETW_:pno,slot,arm[CR]
 '   PRINT("@cmd:@<cmd>");'DEBUG:檢查值
 '   DELAY(100);
    SETVAR("@pno", "1201");
    SETVAR("@slot", "10");
    SETVAR("@arm", "2");
	SETVAR("@device", "Robot01");
	'SETVAR("@cmd", "$1CMD:GETW_:@<pno>,@<slot>,@<arm>");'$1CMD:GETW_:pno,slot,arm[CR]
    SETVARS("@device", "Robot01", "@cmd", "$1CMD:GETW_:@<pno>,@<slot>,@<arm>");
    API("ATEL_ROBOT_MOTION_CMD", True);	
	'PRINT("@value:@<value>");'DEBUG:檢查值
    'Return ("@<ATEL_ROBOT_SET_SPEED_RESULT>");
	
End Function;