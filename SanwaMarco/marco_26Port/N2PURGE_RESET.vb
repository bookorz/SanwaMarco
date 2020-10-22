Function N2PURGE_RESET() As String;
	SETVAR("@device", "N2Purge");
	SETVAR("@cmd", "$1SET:RESET");
    API("N2_PURGE_SET_CMD", True);
End Function;