Function RobotSpeed() AS String;
	
    SETVAR("@value","@<arg1>");
	'Check Value
	RETURN("NAK:VALUE_EMPTY","'@<value>' = 'undefined'");'SPEED 參數未定義
	RETURN("NAK:VALUE_OUT_RANGE,@<value>","(@<value> < 0) or (@<value> > 99)");'SPEED 必須介於 0~99
	'Set Controller
	SETVAR("@device", "Robot01");
	SETVAR("@cmd", "$1SET:SP___:@<value>");
    API("ATEL_ROBOT_SET_CMD", true);	

End Function;

