Function RobotReset() As String;
	SETVAR("@device", "Robot01");
	SETVAR("@cmd", "$1SET:RESET");
    API("ATEL_ROBOT_SET_CMD", True);
End Function;