Function RobotHome() AS String;

    'Set Controller
	SETVAR("@controller","$1");

    API("ATEL_ROBOT_HOME", true);
	
    RETURN("@<ATEL_ROBOT_HOME_RESULT>");

End Function;