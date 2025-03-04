
-- --------------------------------------------------------

--
-- Table structure for table `tournamentsstatus`
--

CREATE TABLE `tournamentsstatus` (
  `tournamentsstatusid` int(10) UNSIGNED NOT NULL,
  `tournamentsstatusdescription` varchar(50) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1 COLLATE=latin1_swedish_ci;

--
-- Dumping data for table `tournamentsstatus`
--

INSERT INTO `tournamentsstatus` (`tournamentsstatusid`, `tournamentsstatusdescription`) VALUES
(1, 'Completed'),
(2, 'Registering'),
(3, 'Announced'),
(4, 'Cancelled'),
(5, 'Playing');
