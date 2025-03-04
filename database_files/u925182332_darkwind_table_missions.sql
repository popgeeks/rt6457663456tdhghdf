
-- --------------------------------------------------------

--
-- Table structure for table `missions`
--

CREATE TABLE `missions` (
  `missionId` int(11) NOT NULL,
  `missionTitle` varchar(100) DEFAULT NULL,
  `missionDescription` varchar(1500) DEFAULT NULL,
  `missionLow` int(11) DEFAULT NULL,
  `missionHigh` int(11) DEFAULT NULL,
  `missionKeyword` varchar(50) DEFAULT NULL,
  `missionMinLevel` int(11) DEFAULT NULL,
  `missionDifficulty` int(11) DEFAULT 1,
  `missionRepeatable` int(11) DEFAULT 0,
  `rewardItemsKey1` varchar(45) DEFAULT NULL,
  `rewardItemsKey2` varchar(45) DEFAULT NULL,
  `rewardItemsKey3` varchar(45) DEFAULT NULL,
  `rewardItemsKey4` varchar(45) DEFAULT NULL,
  `rewardItemsKey5` varchar(45) DEFAULT NULL,
  `rewardItemsKey6` varchar(45) DEFAULT NULL,
  `rewardItemsKey7` varchar(45) DEFAULT NULL,
  `rewardItemsKey8` varchar(45) DEFAULT NULL,
  `rewardItemsKey9` varchar(45) DEFAULT NULL,
  `rewardItemsKey10` varchar(45) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1 COLLATE=latin1_swedish_ci ROW_FORMAT=COMPACT;
