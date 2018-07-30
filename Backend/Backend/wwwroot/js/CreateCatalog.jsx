var accessToken;

class CreateCatalogs extends React.Component {

    constructor(props) {
        super(props);
        this.state = { name: "", title: "TestTitle", pid: "" };
        this.onChange = this.onChange.bind(this);
        this.handleSubmit = this.handleSubmit.bind(this);
        this.onChangeTitle = this.onChangeTitle.bind(this);
        this.onChangePID = this.onChangePID.bind(this);
        this.createCatalog = this.createCatalog(this);
    }

    handleSubmit(e) {
        e.preventDefault();
        alert("Имя: " + this.state.name);
    }

    onChange(e) {
        var val = e.target.value;
        this.setState({ name: val });
    }

    onChangeTitle(e) {
        var val = e.target.value;
        this.setState({ pass: val });
    }

    onChangePID(e) {
        var val = e.target.value;
        this.setState({ pid: val });
    }

    createCatalog(e) {
        var json = JSON.stringify({ Title: this.state.title, ParentsCatalogId: this.state.pid }, null, ' ');
        var request = new XMLHttpRequest();
        request.open("POST", "http://localhost:57059/api/v1/catalogs");
        request.setRequestHeader('Authorization', 'Bearer ' + accessToken);
        request.setRequestHeader('Content-type', 'application/json; charset=utf-8');
        request.onreadystatechange = function () {
            if (request.readyState == 4 && request.status == 201) {
                var temp = JSON.parse(request.responseText);
                alert(temp);
            } else alert(request.status);
        }
        request.send(json);
    }

    render() {
        return (
            <div>
                <input type="text"
                    value={this.state.title}
                    onChange={this.onChangeTitle} />
                <input type="text"
                    value={this.state.pid}
                    onChange={this.onChangePID} />
                <button onClick={this.createCatalog}> Enter </button>
            </div>
        );
    }
}

ReactDOM.render(
    <CreateCatalogs />,
    document.getElementById("content")
);