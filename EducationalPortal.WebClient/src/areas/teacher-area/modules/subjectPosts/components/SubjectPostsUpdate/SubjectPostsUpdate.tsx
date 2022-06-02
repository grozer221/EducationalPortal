import React, {FC, useEffect, useState} from 'react';
import {Form, Input, message, Modal, Select} from 'antd';
import {sizeFormItem} from '../../../../../../styles/form';
import {useMutation} from '@apollo/client';
import {
    UPDATE_SUBJECT_POST_MUTATION,
    UpdateSubjectPostData,
    UpdateSubjectPostVars,
} from '../../../../../../graphQL/modules/subjectPosts/subjectPosts.mutations';
import {SubjectPost, SubjectPostType} from '../../../../../../graphQL/modules/subjectPosts/subjectPosts.types';
import {WysiwygEditor} from '../../../../components/WysiwygEditor/WysiwygEditor';

type Props = {
    isModalPostUpdateVisible: boolean,
    setIsModalPostUpdateVisible: (flag: boolean) => void,
    refetchSubjectAsync: () => void,
    inEditingPost: SubjectPost,
    setInEditingPost: (post: SubjectPost | null) => void,
};

export const SubjectPostsUpdate: FC<Props> = ({
                                                  isModalPostUpdateVisible,
                                                  setIsModalPostUpdateVisible,
                                                  refetchSubjectAsync,
                                                  inEditingPost,
                                                  setInEditingPost,
                                              }) => {
    const [updateSubjectPostMutation, updateSubjectPostMutationOptions] = useMutation<UpdateSubjectPostData, UpdateSubjectPostVars>(UPDATE_SUBJECT_POST_MUTATION);
    const [form] = Form.useForm();
    const [id, setId] = useState('');
    const [title, setTitle] = useState('');
    const [text, setText] = useState('');
    const [type, setType] = useState<SubjectPostType>(SubjectPostType.Info);
    const [init, setInit] = useState(false);

    useEffect(() => {
        setId(inEditingPost.id);
        setTitle(inEditingPost.title);
        setText(inEditingPost.text);
        setType(inEditingPost.type);
        setInit(true);
    }, []);

    const handleOk = async () => {
        try {
            await form.validateFields();
            updateSubjectPostMutation({
                variables: {
                    updateSubjectPostInputType: {
                        id,
                        title,
                        text,
                        type,
                    },
                    withHomeworks: false,
                    withFiles: false
                },
            })
                .then(async (response) => {
                    setInEditingPost(null);
                    setIsModalPostUpdateVisible(false);
                    await refetchSubjectAsync();
                })
                .catch(error => {
                    message.error(error.message);
                });
        } catch (e) {
        }
    };

    const handleCancel = () => {
        setInEditingPost(null);
        setIsModalPostUpdateVisible(false);
    };

    if (!init)
        return null;

    return (
        <Modal
            confirmLoading={updateSubjectPostMutationOptions.loading}
            title="Оновити пост"
            visible={isModalPostUpdateVisible}
            onOk={handleOk}
            okText={'Оновити'}
            onCancel={handleCancel}
            cancelText={'Відміна'}
            width={'70%'}
        >
            <Form
                name="SubjectsPostUpdateForm"
                form={form}
                initialValues={{
                    title: inEditingPost.title,
                    text: inEditingPost.text,
                    type: inEditingPost.type,
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
                >
                    <WysiwygEditor text={text} setText={setText}/>
                </Form.Item>
                <Form.Item
                    name="type"
                    label="Тип"
                    rules={[{required: true, message: 'Введіть тип!'}]}
                >
                    <Select style={{width: '100%'}} value={type} onChange={setType}>
                        {(Object.values(SubjectPostType) as Array<SubjectPostType>).map((value) => (
                            <Select.Option key={value} value={value}>
                                {Object.keys(SubjectPostType)[Object.values(SubjectPostType).indexOf(value)]}
                            </Select.Option>
                        ))}
                    </Select>
                </Form.Item>
            </Form>
        </Modal>
    );
};
