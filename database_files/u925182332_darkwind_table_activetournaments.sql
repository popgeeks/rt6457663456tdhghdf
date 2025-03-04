
-- --------------------------------------------------------

--
-- Table structure for table `activetournaments`
--

CREATE TABLE `activetournaments` (
  `id` int(10) UNSIGNED NOT NULL,
  `game` varchar(45) NOT NULL,
  `type` varchar(45) NOT NULL,
  `prize` int(10) UNSIGNED NOT NULL DEFAULT 0,
  `timestamp` timestamp NOT NULL DEFAULT current_timestamp() ON UPDATE current_timestamp(),
  `games` int(10) UNSIGNED NOT NULL DEFAULT 0,
  `administrator` varchar(45) NOT NULL,
  `finished` int(10) UNSIGNED NOT NULL DEFAULT 0,
  `tournamentname` varchar(500) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1 COLLATE=latin1_swedish_ci;

--
-- Dumping data for table `activetournaments`
--

INSERT INTO `activetournaments` (`id`, `game`, `type`, `prize`, `timestamp`, `games`, `administrator`, `finished`, `tournamentname`) VALUES
(1, 'Omni Triple Triad', 'Single Elimination', 18, '2009-06-29 19:10:00', 3, 'atomicstorm', 1, 'The Legendary is a Drama Queen Tournament'),
(2, 'Omni Triple Triad', 'Single Elimination', 20, '2009-06-29 20:15:09', 1, 'atomicstorm', 1, 'The Atomic Wants to Murder the ISP Tournament'),
(3, 'Triple Triad Memory', 'Single Elimination', 50, '2009-06-29 20:53:01', 3, 'atomicstorm', 1, 'Who Has Brains Tournament?'),
(4, 'Triple Triad', 'Single Elimination', 125, '2009-06-29 22:01:06', 4, 'atomicstorm', 1, 'The GothicKratos Humps Corn Tournament'),
(5, 'Triple Triad War', 'Single Elimination', 50, '2009-06-29 23:02:59', 0, 'atomicstorm', 1, 'Are You Ready for a War Tournament?'),
(6, 'Omni Triple Triad', 'Single Elimination', 150, '2009-06-30 21:55:55', 2, 'atomicstorm', 1, 'The Ifrit3g is underage tournament'),
(7, 'Omni Triple Triad', 'Single Elimination', 200, '2009-07-01 03:24:15', 1, 'atomicstorm', 1, 'The Ode to the Dasani Bottle Tournament'),
(8, 'Triple Triad', 'Single Elimination', 200, '2009-07-01 06:20:03', 3, 'Jinvicious', 1, 'Test Tournament'),
(9, 'Triple Triad', 'Single Elimination', 2000, '2009-07-01 06:49:23', 3, 'Jinvicious', 1, 'LxC Tournament'),
(10, 'Triple Triad', 'Single Elimination', 40, '2009-07-01 11:19:24', 3, 'glacial89', 1, 'Glacial pwn so Much '),
(11, 'Triple Triad', 'Single Elimination', 300, '2009-07-01 16:20:07', 2, 'glacial89', 1, 'Glacial is Sleepy so easy to win tournament '),
(12, 'Triple Triad Memory', 'Single Elimination', 0, '2009-07-01 16:42:23', 3, 'glacial89', 1, 'Brain Training Tournament. '),
(13, 'Triple Triad', 'Single Elimination', 600, '2009-07-01 17:12:27', 2, 'glacial89', 1, 'Totti is a great Soccer Player and great TT player too lol tournament.   '),
(14, 'Triple Triad', 'Single Elimination', 100, '2009-07-01 17:40:19', 1, 'glacial89', 1, 'Tournament of Champions.   '),
(15, 'Triple Triad', 'Single Elimination', 300, '2009-07-02 01:03:19', 2, 'atomicstorm', 1, 'A Random TT Tournament'),
(16, 'Triple Triad War', 'Single Elimination', 100, '2009-07-02 01:48:47', 0, 'atomicstorm', 1, 'War Tournament'),
(17, 'Triple Triad', 'Single Elimination', 400, '2009-07-02 21:08:21', 3, 'glacial89', 1, 'Random Tournament. '),
(18, 'Omni Triple Triad', 'Single Elimination', 150, '2009-07-02 21:35:41', 2, 'glacial89', 1, 'mitsu says, \" never trust a glacial \" tournament'),
(19, 'Triple Triad', 'Single Elimination', 500, '2009-07-02 22:26:08', 4, 'glacial89', 1, '\"LxC The REVENGE!!!\"'),
(20, 'Triple Triad', 'Single Elimination', 100, '2009-07-04 01:14:20', 1, 'atomicstorm', 1, 'Wheres GKs Nuts Tournament'),
(21, 'Omni Triple Triad', 'Single Elimination', 450, '2009-10-17 04:30:03', 8, 'atomicstorm', 1, 'The Ayuna Smells Tournament'),
(22, 'Chinchirorin', 'Single Elimination', 0, '2009-11-02 13:23:53', 0, 'glacial89', 1, 'Free lv 10'),
(23, 'Omni Triple Triad', 'Single Elimination', 0, '2009-11-02 14:03:50', 6, 'glacial89', 1, 'Same as before'),
(24, 'Omni Triple Triad', 'Single Elimination', 750, '2009-11-25 21:51:24', 2, 'Jinvicious', 1, 'Boredom Begone!'),
(25, 'Omni Triple Triad', 'Single Elimination', 0, '2011-05-08 21:58:41', 5, 'atomicstorm', 1, 'Jin Looks Like a Lady'),
(26, 'Triple Triad', 'Single Elimination', 0, '2011-05-30 18:24:33', 1, 'jackie', 1, 'Test for me');
