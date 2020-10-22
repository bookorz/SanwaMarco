Function RobotHome() As String;
	SETVAR("@device", "Robot01");
	'SETVAR("@cmd", "$1CMD:RET__");
    'API("ATEL_ROBOT_MOTION_CMD", True);	
	SETVAR("@cmd", "$1CMD:SHOME");
    API("ATEL_ROBOT_MOTION_CMD", True);	
End Function;