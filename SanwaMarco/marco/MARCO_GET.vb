Function RobotSpeed();
    SETVAR("@device", "DNM01");
    SETVAR("@io", "1900");
    API("I7565DNM_GETIO", True);	
    'PRINT("I7565DNM_GETIO_RETURN:@<I7565DNM_GETIO_RETURN>");'DEBUG:檢查值
    SETVAR("@io", "1900,1901,1902");
    API("I7565DNM_GETIOS", True);
	IF("'@<I7565DNM_GETIOS_RETURN>' <> '001'")
		Return ("IO_CHECK_ERROR_@<io>:@<I7565DNM_GETIOS_RETURN>");
	ENDIF
End Function;