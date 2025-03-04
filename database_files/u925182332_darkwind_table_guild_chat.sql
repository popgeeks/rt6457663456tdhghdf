
-- --------------------------------------------------------

--
-- Table structure for table `guild_chat`
--

CREATE TABLE `guild_chat` (
  `chatId` int(11) NOT NULL,
  `GuildId` varchar(45) NOT NULL,
  `sender` varchar(45) NOT NULL,
  `chatText` varchar(500) DEFAULT NULL,
  `createdDate` datetime DEFAULT NULL,
  `isDeleted` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1 COLLATE=latin1_swedish_ci;

--
-- Dumping data for table `guild_chat`
--

INSERT INTO `guild_chat` (`chatId`, `GuildId`, `sender`, `chatText`, `createdDate`, `isDeleted`) VALUES
(1, '', '', 'Chat offline ', '2015-06-18 10:15:08', 0),
(2, '501st', 'chander', 'hello', '2015-06-18 10:15:38', 0),
(3, '501st', 'sahil', 'whats up', '2015-06-18 10:15:43', 0),
(4, '501st', 'chander', 'Bas kaim 22', '2015-06-18 10:15:54', 0),
(5, '501st', 'sahil', 'ok thats cool', '2015-06-18 10:16:00', 0),
(6, '501st', 'chander', 'bas thk ', '2015-06-18 10:16:05', 0),
(7, '501st', 'sahil', 'chat is working fine, what you think?', '2015-06-18 10:16:26', 0),
(8, '501st', 'chander', 'Yeah ...Godo work done Chander', '2015-06-18 10:16:42', 0),
(9, '501st', 'chander', '*Good', '2015-06-18 10:16:46', 0),
(10, '501st', 'sahil', 'ok then , tx you can do your job', '2015-06-18 10:17:13', 0),
(11, 'Knights of Dawn', 'Zeromus', 'hello', '2016-07-18 23:15:14', 0),
(12, 'Knights of Dawn', 'Zeromus', 'hello', '2016-07-18 23:15:14', 0),
(13, 'Knights of Dawn', 'Zeromus', 'hi', '2023-01-02 10:35:10', 0),
(14, 'Knights of Dawn', 'Zeromus', 'no?', '2023-01-02 10:35:12', 0);
