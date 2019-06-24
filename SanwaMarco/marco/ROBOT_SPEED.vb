Function RobotSpeed() AS String;
	
    'SETVAR("@value","-50");'DEBUG:設定值測試
	'Check Value
	RETURN("NAK:VALUE_EMPTY","'@<value>' = 'undefined'");'SPEED 參數未定義
	RETURN("NAK:VALUE_OUT_RANGE,@<value>","(@<value> < 0) or (@<value> > 99)");'SPEED 必須介於 0~99
	'Set Controller
	SETVAR("@controller","$1");	
    API("ATEL_ROBOT_SET_SPEED", true);	
	'PRINT("@value:@<value>");'DEBUG:檢查值
    RETURN("@<ATEL_ROBOT_SET_SPEED_RESULT>");
	
End Function;

