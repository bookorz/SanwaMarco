Function RobotReset() AS String;
	SETVAR("@device", "Robot01");
	SETVAR("@cmd", "$1SET:SERVO:1");
    API("ATEL_ROBOT_SET_CMD", True);
End Function;