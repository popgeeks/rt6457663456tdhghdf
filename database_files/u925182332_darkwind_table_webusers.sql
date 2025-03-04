
-- --------------------------------------------------------

--
-- Table structure for table `webusers`
--

CREATE TABLE `webusers` (
  `ID` int(10) UNSIGNED NOT NULL,
  `player` varchar(45) NOT NULL,
  `lastactivity` datetime NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1 COLLATE=latin1_swedish_ci;
