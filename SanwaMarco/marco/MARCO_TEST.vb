Function RobotSpeed() AS String;
	
    'SETVAR("@value","-50");'DEBUG:設定值測試
	'Check Value
	RETURN("NAK:VALUE_EMPTY", "'@<id>' = 'undefined'");'SPEED 參數未定義
	RETURN("NAK:VALUE_EMPTY", "'@<id>' <> '12345'");'SPEED 參數未定義
	'Set Controller
	'SETVAR("@controller","$1");	
    'API("ATEL_ROBOT_SET_SPEED", true);	
	'PRINT("@value:@<value>");'DEBUG:檢查值
    'Return ("@<ATEL_ROBOT_SET_SPEED_RESULT>");
	
End Function;