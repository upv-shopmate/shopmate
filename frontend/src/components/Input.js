/* eslint react/prop-types: 0 */
import React from 'react';
import KeyboardedInput from 'react-touch-screen-keyboard';
import 'react-touch-screen-keyboard/lib/Keyboard.css';

class Input extends React.Component {
  constructor(props) {
    super(props);
    this.state = {
      'value': '',
    };
    this.changeFieldValue = this.changeFieldValue.bind(this);
  }

  onChange(key) {
    console.log(key);
  }

  clearFieldValue() {
    this.setState({
      value: '',
    });
  }

  changeFieldValue(value) {
    this.props.onChangeParent(value);
    this.setState({ value });
  }

  render() {
    const CustomMapping = [
      ['q', 'w', 'e', 'r', 't', 'y', 'u', 'i', 'o', 'p'],
      ['a', 's', 'd', 'f', 'g', 'h', 'j', 'k', 'l', 'ñ'],
      ['z', 'x', 'c', 'v', 'b', 'n', 'm'],
    ];

    return (
      <KeyboardedInput
        enabled
        required
        onChange={(value) => {
          this.changeFieldValue(value);
        }}
        type={this.props.type}
        value={this.state.value}
        placeholder={this.props.placeholder}
        defaultKeyboard={CustomMapping}
        isFirstLetterUppercase={true}
        isDraggable={false}
        opacity={1}
        stickToBottom={true}
      />
    );
  }
}

export default Input;
