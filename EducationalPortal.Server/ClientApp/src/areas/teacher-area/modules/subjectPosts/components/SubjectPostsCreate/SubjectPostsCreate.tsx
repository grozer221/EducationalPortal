import React, {FC, useState} from 'react';
import {Form, Input, message, Modal, Select} from 'antd';
import {sizeFormItem} from '../../../../../../styles/form';
import {useMutation} from '@apollo/client';
import {CREATE_SUBJECT_POST_MUTATION, CreateSubjectPostData, CreateSubjectPostVars} from '../../subjectPosts.mutations';
import {SubjectPostType} from '../../subjectPosts.types';

type Props = {
    isModalPostCreateVisible: boolean,
    setIsModalPostCreateVisible: (flag: boolean) => void,
    subjectId: string,
    refetchSubjectAsync: () => void,
};

export const SubjectPostsCreate: FC<Props> = ({
                                                  isModalPostCreateVisible,
                                                  setIsModalPostCreateVisible,
                                                  subjectId,
                                                  refetchSubjectAsync,
                                              }) => {
    const [createSubjectPostMutation, createSubjectPostMutationOptions] = useMutation<CreateSubjectPostData, CreateSubjectPostVars>(CREATE_SUBJECT_POST_MUTATION);
    const [form] = Form.useForm();
    const [title, setTitle] = useState('');
    const [text, setText] = useState('');
    const [type, setType] = useState<SubjectPostType>(SubjectPostType.Info);

    const handleOk = async () => {
        try {
            await form.validateFields();
            console.log(title, text, type, subjectId);
            createSubjectPostMutation({
                variables: {
                    createSubjectPostInputType: {
                        title,
                        text,
                        type,
                        subjectId,
                    },
                },
            })
                .then(async (response) => {
                    setIsModalPostCreateVisible(false);
                    setTitle('');
                    setText('');
                    setType(SubjectPostType.Info);
                    await refetchSubjectAsync();
                })
                .catch(error => {
                    message.error(error.message);
                });
        } catch (e) {
        }
    };

    const handleCancel = () => {
        setIsModalPostCreateVisible(false);
        form.resetFields();
    };

    return (
        <Modal
            confirmLoading={createSubjectPostMutationOptions.loading}
            title="Створити пост"
            visible={isModalPostCreateVisible}
            onOk={handleOk}
            okText={'Створити'}
            onCancel={handleCancel}
            cancelText={'Відміна'}
            width={'70%'}
        >
            <Form
                name="SubjectsPostCreateForm"
                form={form}
                initialValues={{
                    type: type,
                }}
                {...sizeFormItem}
            >
                <Form.Item
                    name="title"
                    label="Заголовок"
                    rules={[{required: true, message: 'Введіть Заголовок!'}]}
                >
                    <Input placeholder="Заголовок" value={title} onChange={e => setTitle(e.target.value)}/>
                </Form.Item>
                <Form.Item
                    name="text"
                    label="Текст"
                    rules={[{required: true, message: 'Введіть Текст!'}]}
                >
                    <Input placeholder="Текст" value={text} onChange={e => setText(e.target.value)}/>
                </Form.Item>
                <Form.Item
                    name="type"
                    label="Тип"
                    rules={[{required: true, message: 'Введіть Тип!'}]}
                >
                    <Select style={{width: '100%'}} value={type} onChange={setType}>
                        {(Object.values(SubjectPostType) as Array<SubjectPostType>).map((value) => (
                            <Select.Option
                                value={value}>{Object.keys(SubjectPostType)[Object.values(SubjectPostType).indexOf(value)]}</Select.Option>
                        ))}
                    </Select>
                </Form.Item>
            </Form>
        </Modal>
    );
};
