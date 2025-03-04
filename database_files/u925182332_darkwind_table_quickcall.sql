
-- --------------------------------------------------------

--
-- Table structure for table `quickcall`
--

CREATE TABLE `quickcall` (
  `keyword` varchar(30) NOT NULL DEFAULT '',
  `description` text NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1 COLLATE=latin1_swedish_ci;

--
-- Dumping data for table `quickcall`
--

INSERT INTO `quickcall` (`keyword`, `description`) VALUES
('captainobvious', 'This Captain Obvious moment is brought to you by Ebay.. the only place you can actually buy a brain at a discount price!'),
('cash', 'Your cash account can grow by doing our rewards program or by donating cash to us without tieing it into a membership.  Visit our membership page for details.'),
('championspot', 'A win in a pot based tournament will get you 1 entry into the champions pot drawing that is 20% of all fees taken from the pot tournaments all week long!  The pot can be hundreds of GP that you can use to buy cards!'),
('complaints', 'Complaints regarding Dark Wind are to be made on the Pest Control forum board.  This will be displayed for all users to comment on to address issues.  If you aren\'t on the forums, register and be a part of the community.'),
('configuration', 'Because v2 is installed in a different folder and if you did not specify this, you will have to reconfigure your options such as sounds, logins/parts, etc.  To do this go to Game -> Configuration Window to make your changes.'),
('content', 'Please be sure to keep chat content to subjects suitable for all ages... all adult content will be warned/kicked/or banned at the disgression of an admin. Please keep TTO a place where all ages can play together. Also see \"cussing.\"'),
('disappearingcards', 'Got problems with disappearing cards and know people with install problems? Have them download our newest 13.0mb setup build.  For details: http://www.tripletriadextreme.com/forums/showthread.php?p=12394#post12394'),
('divorce', 'You can divorce another character and remove the surname that you had acquired.  Just petition it to a GM.'),
('endgames', 'A reminder to all: When requesting an endgame, please PM a Game Master (GM) with a valid reason, and an administrator will handle it. Losing is not a valid reason.  Also note that if the player has been inactive for more than 4 minutes, you can request an endgame and the game will auto-terminate.  Does not apply to Team OTT or Sphere Break.'),
('eventcards', 'All event cards except for the new decks and FFAC and Nintendo are now rare event cards.  They can no longer obtained through memberships.'),
('filterabuse', 'Over use of the filter may result in being kicked, banned, or permanent loss of account.  It is there for the occasional mess ups; however, over usage violates our TOS.'),
('forums', 'Forums are a great way for people to communicate off the game, while you are work, or to get in touch with friends that aren\'t on the same time as you.  Make an account with the same name as your game character to get full benefits, stats, and more!'),
('fulldeck-tt', 'Did you know that you can play Triple Triad with any group of our almost 1500 cards? The fulldeck trade rule will allow you to play like OTT random.  Note that it is immediately trade none!  Triple Triad to a whole new level!'),
('gifts', 'Want to buy a membership for another player? Go to the product store in the portal and buy the player a gift certificate.  The player can then go to the store and use it for anything in the shop.'),
('halloween', 'Get your Halloween Special Edition cards before November 30th! That will be the last date these cards can be purchased until 2010!'),
('helpwiki', 'Too many features for you to figure out? Go to the help wiki on the portal for more information!'),
('install-trouble', 'Got friends that are having trouble logging into v2 because the game is freezing on login.  View our solution here: http://www.tripletriadextreme.com/forums/showthread.php?p=12382#post12382'),
('manual', 'Training Manual -> http://www.tripletriadextreme.com/trainer.php?area=index'),
('memberships', 'The membership benefits have upgraded! Get yours today!http://www.tripletriadextreme.com/support.php?area=membership'),
('pipedown', 'The terms of service contains the rules and guidelines of Dark Wind Online which you agree to by logging into Dark Wind.  This includes general legal disclaimers, rules of conduct, and disciplinary matters.  Please make sure you visit if you haven\'t by going to: http://www.tripletriadextreme.com/support.php?area=tos'),
('playershops', 'You can view your player shops on the web at http://www.tripletriadextreme.com/playershops.php'),
('poweruser', 'By buying a power user membership, you help TTE grow and allow us to produce prized tournaments and drawings.  You get 10000GP, 10000AP, 4 Tokens, and More!'),
('specialedition', 'Get special edition cards that cannot be lost in games and hold unimaginable powers. Visit [ http://www.tripletriadextreme.com/support.php?area=membership ] for more details!'),
('staffdemands', 'We need graphic artists! If you wish to apply, email darklumina13@darkwindonline.com with LINKS (no attachments) to your artwork.'),
('suggestions', 'Suggestions for Dark Wind are to be made on the Construction Suggestions forum board.  This will be displayed for all users within the community to expand and elaborate on.'),
('ticket', 'To submit a ticket, submit a bug, or recover lost passwords - Go to Customer Support on the website at http://www.tripletriadextreme.com/support/  - No work will be done from any of the support staff unless a ticket has been created.'),
('toc', 'Tournament of Champion Rules -> Immune/Same/Plus/Random/Combo Trade: None'),
('tokens', 'Tokens are a new currency with v2 that allow membership holders to redeem their cards at any time.  This also means they can save up for more.  In future versions, you will be able to redeem your dark wind cash into tokens.  Help make Dark Wind grow!'),
('ttecashprogram', 'Now there is a new program available where you can earn TTE Cash Daily by completing surveys daily, completing offers, doing quizzes, or even purchasing TTE cash points by a variety of new payment methods.  Click the following link for more details: http://www.tripletriadextreme.com/programs/EarnTTECash.aspx - (Note: You will not get credit if you try to give bogus information, bad emails, or fraudulent information.. doing so can get your account banned if it is reported by the third party as it is the same as fiscal fraud).'),
('v2', 'Satisified with v2 and want more? Help us out financially but getting a membership.  Get many benefits and take your game play to another level.  http://www.tripletriadextreme.com/support.php?area=membership'),
('webportal', 'Visit the new Web Portal (Game>Web-based Game Portal) for stats, leaderboards, brand new infomation and the latest developments in TTE!');
