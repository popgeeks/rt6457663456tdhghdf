
-- --------------------------------------------------------

--
-- Table structure for table `weblogin`
--

CREATE TABLE `weblogin` (
  `ID` int(10) UNSIGNED NOT NULL,
  `PLAYER` varchar(20) NOT NULL,
  `LASTACTIVITY` timestamp NOT NULL DEFAULT current_timestamp() ON UPDATE current_timestamp(),
  `LASTIP` varchar(20) NOT NULL,
  `USERTOKEN` varchar(45) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1 COLLATE=latin1_swedish_ci ROW_FORMAT=COMPACT;
