Function RobotReset() AS String;

	SETVAR("@controller","$1");
    API("ATEL_ROBOT_RESET", true);
	
	RETURN("@<ATEL_ROBOT_RESET_RESULT>");

End Function;