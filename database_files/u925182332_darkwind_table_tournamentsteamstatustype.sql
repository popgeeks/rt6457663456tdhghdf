
-- --------------------------------------------------------

--
-- Table structure for table `tournamentsteamstatustype`
--

CREATE TABLE `tournamentsteamstatustype` (
  `TournamentsTeamStatusTypeID` int(10) UNSIGNED NOT NULL,
  `TournamentsTeamStatusType` varchar(45) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1 COLLATE=latin1_swedish_ci;

--
-- Dumping data for table `tournamentsteamstatustype`
--

INSERT INTO `tournamentsteamstatustype` (`TournamentsTeamStatusTypeID`, `TournamentsTeamStatusType`) VALUES
(0, 'Normal'),
(1, 'Attacker'),
(2, 'Healer');
