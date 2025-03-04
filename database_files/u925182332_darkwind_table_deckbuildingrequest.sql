
-- --------------------------------------------------------

--
-- Table structure for table `deckbuildingrequest`
--

CREATE TABLE `deckbuildingrequest` (
  `ID` int(11) NOT NULL,
  `RequestedPlayer` varchar(45) NOT NULL,
  `RequestedDate` varchar(30) NOT NULL,
  `IsApproved` tinyint(3) NOT NULL DEFAULT 0,
  `ApprovedBy` varchar(45) DEFAULT NULL,
  `ApprovedDate` varchar(30) DEFAULT NULL,
  `PlayerComment` varchar(200) DEFAULT NULL,
  `AdminComment` varchar(200) DEFAULT NULL,
  `IsDeleted` tinyint(3) DEFAULT 0,
  `DeletedBy` varchar(45) DEFAULT NULL,
  `DeletedDate` varchar(30) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1 COLLATE=latin1_swedish_ci;

--
-- Dumping data for table `deckbuildingrequest`
--

INSERT INTO `deckbuildingrequest` (`ID`, `RequestedPlayer`, `RequestedDate`, `IsApproved`, `ApprovedBy`, `ApprovedDate`, `PlayerComment`, `AdminComment`, `IsDeleted`, `DeletedBy`, `DeletedDate`) VALUES
(1, 'forumadmin', '2021-08-06', 1, 'forumadmin', '2021-08-06', 'Request access to create deck', '', 0, NULL, NULL),
(2, 'Cloud7777777', '2022-03-29', 0, NULL, NULL, 'How do i play with the cards i just got?', NULL, 0, NULL, NULL),
(3, 'tukty', '2022-04-01', 0, NULL, NULL, '', NULL, 0, NULL, NULL),
(4, 'Vey', '2022-09-26', 0, NULL, NULL, '', NULL, 0, NULL, NULL);
