
-- --------------------------------------------------------

--
-- Structure for view `vw_randomcardsbylevel4`
--
DROP TABLE IF EXISTS `vw_randomcardsbylevel4`;

CREATE ALGORITHM=UNDEFINED DEFINER=`u925182332_darkwind`@`localhost` SQL SECURITY DEFINER VIEW `vw_randomcardsbylevel4`  AS SELECT `cards`.`id` AS `ID`, rand() AS `rand()` FROM `cards` WHERE `cards`.`level` = 4 ORDER BY rand() ASC LIMIT 0, 10 ;
