
-- --------------------------------------------------------

--
-- Table structure for table `webguests`
--

CREATE TABLE `webguests` (
  `ID` int(11) NOT NULL,
  `UserAuth` varchar(500) DEFAULT NULL,
  `LastActivity` varchar(45) DEFAULT NULL,
  `LastIP` varchar(45) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1 COLLATE=latin1_swedish_ci ROW_FORMAT=COMPACT;
