Function RobotReset() AS String;
	SETVAR("@device", "Robot01");
	SETVAR("@cmd", "$1SET:RESET");
    API("ATEL_ROBOT_SET_CMD", True);
	SETVAR("@cmd", "$1SET:SERVO:1");
    API("ATEL_ROBOT_SET_CMD", True);
	SETVAR("@cmd", "$1SET:MODE_:0");
    API("ATEL_ROBOT_SET_CMD", True);
	SETVAR("@cmd", "$1SET:SP___:30");
    API("ATEL_ROBOT_SET_CMD", True);
	SETVAR("@cmd", "$1CMD:RET__");
    API("ATEL_ROBOT_MOTION_CMD", True);	
End Function;