
-- --------------------------------------------------------

--
-- Table structure for table `textlinks`
--

CREATE TABLE `textlinks` (
  `ID` int(10) UNSIGNED NOT NULL,
  `Text` text NOT NULL,
  `Active` int(10) UNSIGNED NOT NULL DEFAULT 0,
  `Views` int(10) UNSIGNED NOT NULL DEFAULT 0
) ENGINE=InnoDB DEFAULT CHARSET=latin1 COLLATE=latin1_swedish_ci;

--
-- Dumping data for table `textlinks`
--

INSERT INTO `textlinks` (`ID`, `Text`, `Active`, `Views`) VALUES
(1, 'Satisified with v2 and want to help out for v3? Help us out financially but getting a membership.  Get many benefits and take your game play to another level.  http://www.tripletriadextreme.com/support.php?area=membership', 0, 643),
(2, 'Don\'t like the v2 skin? Create your own! Send a message to Jinx to tell you how or visit our forums by clicking on the following link: http://www.tripletriadextreme.com/forums/showthread.php?t=1431&page=1&pp=10  -- This will show you what the community is doing with skin design.', 0, 0),
(3, 'Did you know that dark wind\'s advertisements on our website provide exponential help in bringing endless entertainment to you every month? Well they do! Visit them and show them that you also support the products that they offer.', 0, 0),
(4, 'Did you know that you could view the cards that you are missing and know how far you are away from 100% completion?  Well you can! Go to the card viewer and click on the deck name which is in a blue link', 0, 0),
(5, 'Did you know that you can play Triple Triad with any group of our almost 1500 cards? The fulldeck trade rule will allow you to play like OTT random.  Note that it is immediately trade none!  Triple Triad to a whole new level!', 0, 0),
(6, 'By buying a power user membership, you help TTE grow and allow us to produce prized tournaments and drawings.  You get 6000GP/AP, 1 Special Edition card which you can never lose, and 25% extra EXP and 50% GP!  If you renew it every month, you will also get the same benefits (GP/AP/Cards/etc) every month + 10% GP/AP as a thank you for supporting us.  So buy one today!', 0, 0),
(7, 'A reminder to all: When requesting an endgame, please PM a Game Master (GM) with a valid reason, and an administrator will handle it. Losing is not a valid reason.  Also note that if the player has been inactive for more than 4 minutes, you can request an endgame and the game will auto-terminate.  Does not apply to Team OTT or Sphere Break.', 0, 233),
(8, 'New to V2! View the auctions by going to Game>Auctions. Or auction a card of your own through the Card Viewer. When bidding on an auction your money will be held until you are outbid or win the auction.  Consider it DW-Bay :D', 0, 0),
(9, 'A client update will be available tonight to address player shops and the bazaar search.  Estimated time will be 10pm CST (GMT-6)', 0, 0),
(10, 'v2.0.7.757 is available if you have not already run the updater.  The following is a list of changes: http://www.tripletriadextreme.com/forums/showthread.php?p=12461#post12461', 0, 0),
(11, 'Having trouble logging in sometimes? If you get the message that \"there is a user already online with this nickname\" for more than 30 minutes, please post in this thread. Admins will be monitoring it. I am aware of this issue and will have it corrected by the weekend. -- http://www.tripletriadextreme.com/forums/showthread.php?p=12494#post12494', 0, 0),
(12, 'Did you know you can convert your Dark wind virtual cash into memberships, tokens, and more? Just accumulate cash by going to Website -> Dark Wind Rewards and fulfilling offers on that page.  Please read the rules and what to do on that page.  To redeem, send a ticket saying what you want to do.  $5 = 1 Token or convert to a membership', 0, 0),
(13, 'The Christmas Event Cards will be going out of circulation on February 1st (that means the last day is Jan 31st).  Make sure you go and get them before its too late! To purchase, you can get any event card on our website and we will give you 4 tokens to redeem as you wish.  $10 per card (email for bulk prices)', 0, 0),
(14, 'Get your Christmas Cards now before its too late!  The cards will be taken out of the token redemption center at midnight of January 31st or Wednesday evening CST (GMT -6).  To buy, just get any random event card on our membership page or purchase a membership.  You will get tokens if you just get an event card and you can redeem it easily!', 0, 0),
(15, 'From March 14th to March 31st, you can get all the old event cards through buying tokens at our membership website on our front page.  Getting a power user membership means you get 5 tokens during this period.  Dark Wind Online celebrating 3 years!', 0, 0),
(16, 'New Dark Wind Rewards Offers are available now!  Go to the Dark Wind rewards website (from the game, if you can) and fill them out.  You will be credited when we are credited for your completion.  If you dont use your name, make sure you get the tracking # from the site.', 0, 0),
(17, 'A new line of cards (currently only 1) called Mythical Cards have been released.  Each of these cards cost 8 tokens and must be redeemed through sending a ticket.  These cards are very strong but every strong card has its own weakness.  On sale for limited time only!', 0, 0),
(18, 'Chat content is moderated.  Per COPPA, ALL CHAT content must be appropriate for players under the age of 13.  Excessive swearing, foul language, crude (sexual or racist) comments, and badmouthing a GM/admin, you may be kicked or banned permanently.  See the terms of service document on the website.', 1, 23488),
(19, 'Veteran Abilities are abilities that help you progress your character through the memberships that you buy.  1 VA point is 3000 xp and $1 gets you 100 points.  You will never lose these points.', 0, 0),
(20, 'Membership benefits have now increased! Get yours today! http://www.tripletriadextreme.com/support.php?area=membership', 0, 5030),
(21, 'Triple Triad Extreme earns revenue to power our servers and website content by your loyal membership subscriptions and the sponsors that are on our web site.  Thanks for helping the community grow!', 1, 23583),
(22, 'Version 3 has been announced! Go here to read the forum post: http://www.tripletriadextreme.com/forums/local-pub/3253-version-3-announcement.html#post21124', 0, 3499),
(23, 'Don\'t have any money to buy a membership but want one? You can now participate in new Quests on the Game Portal (www.tripletriadoffline.com/Portal/Quests.aspx).', 0, 4564),
(25, 'Did you know that there are many in-game features in the http://www.tripletriadoffline.com/Portal/ - game portal? Tournaments, leaderboards, achievement lists, and more are all there and future developments will be there.  Make it a point to visit daily!', 0, 4341),
(26, 'Get your Halloween Special Edition cards before November 13th! That will be the last date these cards can be purchased until 2009!', 0, 1254),
(27, 'On January 13th, 2009, 55 New Cash Quests Have Been Added! Go to the Portal and Check it out! http://www.tripletriadoffline.com/Portal/', 0, 1307),
(28, 'You can now buy gold, ap, tokens, and gift certificates in the membership page on the Game Portal.  Memberships and cart items are now automatically credited to your account! Go to http://www.tripletriadoffline.com/Portal/Products.aspx for more information!', 0, 3333),
(29, 'Signup for the February Tournament of Champions Today! Registration Ends February 13th. http://www.tripletriadextreme.com/forums/triple-triad-extreme-tournament-champions/', 0, 219),
(30, 'www.carbogle.com has just launched.  It is a newly released google front-end that helps TTE grow and to do it in a humorous way.  Bookmark it and use it for your searches!', 0, 87),
(31, 'Signup for our email newsletter that contains new update information about Triple Triad Extreme! Go here: http://feedburner.google.com/fb/a/mailverify?uri=TripleTriadExtreme&amp;loc=en_US -- enter your email address and sign up!', 0, 92),
(32, 'Did you know you get AP for forum activity now? See this thread for details: http://www.tripletriadextreme.com/forums/triple-triad-pub/11615-forum-activity-gives-you-game-ap.html', 0, 623),
(33, 'You can buy loot cards in the products page (http://www.tripletriadoffline.com/Portal/Products.aspx) - and even sell them in your player shop! Memberships too!', 0, 342),
(34, 'Triple Triad Extreme Needs Your Help! Get a Power User Membership and Earn Faster GP, Faster EXP, and more! You cant say no to 80GP a game! http://www.tripletriadoffline.com/Products.aspx for more information!', 0, 13887),
(35, 'You can now get 20% extra AP and GP if you keep your Power User membership and it renews!  It is yet another thank you for supporting Triple Triad Extreme!', 0, 13950),
(36, 'The token costs for each card has changed to pre-lu03 levels.  It was a mistake and it has been corrected.  Sorry for the inconvenience.', 0, 219),
(37, 'Is your geepee low? Did you know that by buying Pandora Redemption (Token Loot Cards) and Essence Loot Cards (memberships), you can sell them in your shop and get up to 60,000 gold for only 20 bucks? Its true! Go here: http://www.tripletriadextreme.com/Store/Membership-Compare.aspx', 1, 18497),
(38, 'Got nothing better to do with your RAM and CPU? Did you know that your rush gauge does not decay by staying online? Its true! (Effective May 16th)', 0, 199),
(39, 'Latest Patch Notes: http://www.tripletriadextreme.com/final-fantasy-triple-triad-forums/triple-triad-pub/13445-portal-update-may-16th-2009-a.html', 0, 188),
(40, 'Effective LU03, VIP status is just a way to reward players with power users for helping us keep Triple Triad Extreme going. A power user membership is required to qualify for any of the 4 tiers of VIP.  See http://www.tripletriadoffline.com/Portal/Products.aspx for more information.', 0, 1965),
(41, 'For the Month of October, Receive a Pandoras Redemption Loot Card (1 Token) for New and EXISTING Power User Subscriptions.  See http://www.tripletriadextreme.com/final-fantasy-triple-triad-forums/triple-triad-pub/13715-june-rewards.html#post37570 for more details.', 0, 1797),
(42, 'The advertisements on our website, portal, and forums allow us to grow and invest to bring in new players.  Advertisements provide us with revenue for prizes, server costs, and funds to invest in new games and features.', 0, 323),
(43, 'Want a shot at a power user loot card? Join the September Tournament of Champions tournament.  Signup here: http://www.tripletriadforums.com/triple-triad-extreme-tournament-champions/14999-september-tournament-champions-registration.html', 0, 440),
(44, 'You can now enchant your tokens into loot cards to sell in your shop.  Click here for more information: http://www.tripletriadextreme.com/PageNews/Want-to-sell-your-tokens-portal-update-august-21-2009.aspx', 0, 705),
(45, 'Now there is a new program available where you can earn TTE Cash Daily by completing surveys daily, completing offers, doing quizzes, or even purchasing TTE cash points by a variety of new payment methods.  Click the following link for more details: http://www.tripletriadextreme.com/Player/RewardCenter.aspx', 1, 4038),
(46, 'For the month of November, all players who purchase 1 diamond or power user will receive an entry into the Tournament of Champions tournament at the end of the month for a $50 EBay gift certificate on December 5th at 10am CDT (GMT-6) (has to be this weekend due to Thanksgiving).', 0, 763),
(47, 'Vote now on the next deck to be added to Triple Triad Extreme: http://www.tripletriadforums.com/triple-triad-pub/16211-what-deck-do-you-want-next.html -- This is a 5 day poll! Tell everyone to vote now before it is too late!', 0, 62),
(48, 'The summer project has begun and we need your support. For a limited time, all non-subscription and non-charity items are 10% off AND VIP 3 and 4 memberships get a huge boost. Help us provide you with the best new game possible!', 0, 4647);
