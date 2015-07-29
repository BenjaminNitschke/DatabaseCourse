<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Pong High Scores</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <h2>Pong High Scores</h2>
    </div>
    	<asp:SqlDataSource ID="SqlHighScoreDataSource" runat="server"
				ConnectionString="<%$ ConnectionStrings:PongConnectionString %>"
				SelectCommand="SELECT Username, [Score], [PositionInRanking] FROM [HighScore] JOIN Player On Player.Id = PlayerId ORDER BY [PositionInRanking]"></asp:SqlDataSource>
			<asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataSourceID="SqlHighScoreDataSource" AllowPaging="True" AllowSorting="True" Height="161px" Width="834px">
				<Columns>
					<asp:BoundField DataField="PositionInRanking" HeaderText="Global Rank" SortExpression="PositionInRanking" />
					<asp:BoundField DataField="Username" HeaderText="Player" SortExpression="Username" />
					<asp:BoundField DataField="Score" HeaderText="Score" SortExpression="Score" />
				</Columns>
			</asp:GridView>
    </form>
</body>
</html>
