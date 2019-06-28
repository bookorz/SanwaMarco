Function RobotSpeed();
    SETVAR("@device", "DNM01");
    SETVAR("@io", "1900");
    SETVAR("@value", "1");
    API("I7565DNM_GETIO", True);
    API("I7565DNM_SETIO", True);
    API("I7565DNM_GETIO", True);	
    PRINT("I7565DNM_GETIO_RETURN:@<I7565DNM_GETIO_RETURN>");'DEBUG:檢查值
    SETVAR("@io", "1900,1901,1902");
    API("I7565DNM_GETIOS", True);
    PRINT("I7565DNM_GETIOS_RETURN:@<I7565DNM_GETIOS_RETURN>");'DEBUG:檢查值
	'IF("'@<I7565DNM_GETIOS_RETURN>' <> '001'")
        'Return ("IO_CHECK_ERROR_@<io>:@<I7565DNM_GETIOS_RETURN>");
    'ENDIF
    'SETVAR("@io", "1900,1901,1902");
	SETVAR("@interval", "100");
	SETVAR("@retry_count", "10");
	SETVAR("@values", "000");
    API("I7565DNM_CHECK_IOS", True);
    PRINT("I7565DNM_CHECK_IOS_RETURN:@<I7565DNM_CHECK_IOS_RETURN>");'DEBUG:檢查值
End Function;