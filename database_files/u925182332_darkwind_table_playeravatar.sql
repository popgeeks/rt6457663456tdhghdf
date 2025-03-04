
-- --------------------------------------------------------

--
-- Table structure for table `playeravatar`
--

CREATE TABLE `playeravatar` (
  `playername` varchar(50) NOT NULL,
  `gender` varchar(10) NOT NULL,
  `frameImagePath` varchar(50) NOT NULL,
  `hairImagePath` varchar(50) NOT NULL,
  `clothImagePath` varchar(50) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1 COLLATE=latin1_swedish_ci;

--
-- Dumping data for table `playeravatar`
--

INSERT INTO `playeravatar` (`playername`, `gender`, `frameImagePath`, `hairImagePath`, `clothImagePath`) VALUES
('sahil', 'male', 'background_006.gif', 'male_head_006.gif', 'male_body_004.gif'),
('007clown', 'female', 'background_020.gif', 'female_head_002.gif', 'female_body_003.gif'),
('forumadmin', 'male', 'background_015.gif', 'female_head_002.gif', 'male_body_009.gif'),
('tzzedd', 'male', 'background_015.gif', 'male_head_003.gif', 'male_body_025.gif'),
('Zeromus', 'male', 'background_003.gif', 'male_head_022.gif', 'male_body_007.gif'),
('GothicKratos', 'female', 'background_024.gif', 'male_head_037.gif', 'female_body_013.gif'),
('chander', 'male', 'background_001.gif', 'female_head_009.gif', 'female_body_005.gif'),
('slithered', 'female', 'background_016.gif', 'male_head_033.gif', 'female_body_008.gif');
