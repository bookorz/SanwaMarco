Function RobotOrg() AS String;
	'Set Controller
    SETVAR("@controller","$1");

    API("ATEL_ROBOT_ORG", true);
	
    RETURN("@<ATEL_ROBOT_ORG_RESULT>");

End Function;