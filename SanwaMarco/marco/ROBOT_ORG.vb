Function RobotOrg() AS String;
	SETVAR("@device", "Robot01");
	SETVAR("@cmd", "$1CMD:ORG__");
    API("ATEL_ROBOT_MOTION_CMD", True);	
End Function;